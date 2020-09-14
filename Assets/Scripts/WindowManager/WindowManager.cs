using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class WindowManager : MonoBehaviour
{
    public Action<IViewUI, ScreenId> OnCloseView = delegate { };
    public Action<IViewUI, ScreenId> OnCloseScreenView = delegate { };
    public Action<IViewUI, ScreenId> OnCloseWindowView = delegate { };

    [SerializeField] Canvas canvas;
    [SerializeField] private Canvas Canvas => canvas;
    [SerializeField] private RectTransform screenRect;
    [SerializeField] private RectTransform modalRect;
    [SerializeField] private TitlePanelUIView titlePanelUIView;

    public AbstractView currentMainScreen; //Главный экран
    public AbstractView currentWindow; //Текущее окно в работе
    public AbstractView currentScreen; //Текущий экран в работе

    private List<AbstractView> stackScreen; //Очередь экранов
    private List<AbstractView> stackTutorialScreen; //Очередь экранов
    private List<GameObject> prefabs; //Пул префабов для скорости
    private Action closedActionMain;
    private Action destroyActionMain;

    private ITitlePanelUIController titlePanelUIController;

    public Dictionary<object, ViewElementData> OpenedScreens;
    public Dictionary<object, ViewElementData> OpenedWindows;
    public Dictionary<object, ViewElementData> OpenedTutorials;

    /// <summary>
    /// Открыто любое окно или экран
    /// </summary>
    public bool IsOpenModalOrScreen
    {
        get
        {
            return IsAnyOpenScreen || IsOpenModal || IsOpenTutorial;
        }
    }

    public bool IsOpeneMainScreen
    {
        get
        {
            return currentMainScreen != null;
        }
    }

    /// <summary>
    /// Открыт любой экран
    /// </summary>
    public bool IsAnyOpenScreen
    {
        get
        {
            return currentScreen != null;
        }
    }

    /// <summary>
    /// Открыто любое окно
    /// </summary>
    public bool IsOpenModal
    {
        get
        {
            return OpenedWindows != null && OpenedWindows.Count > 0;
        }
    } 
    
    public bool IsOpenTutorial
    {
        get
        {
            return OpenedTutorials != null && OpenedTutorials.Count > 0;
        }
    }

    /// <summary>
    /// Получает префаб по имени (из пула или грузит из ресурсов)
    /// </summary>
    /// <param name="name">Имя префаба</param>
    /// <returns>Префаб</returns>
    public GameObject GetPrefab(string id, string tag)
    {
        GameObject result = null;
        var fullName = tag + " " + id;
        result = prefabs.Find(p => p.name.ToLower() == fullName.ToLower());

        if (result == null)
        {
            result = Resources.Load<GameObject>("UI/" + fullName);
            prefabs.Add(result);
        }

        return result;
    }

    public GameObject GetPrefab(ScreenId id, string tag)
    {
        id.UsagesCount++;
        return GetPrefab(id.value, tag);
    }

    #region Работа с экранами
    /* Работа с экранами несколько отличается от работы модальных окон
        * Модальное окно может быть в единсвенном колличестве и они в очереди открытия, одно за другим
        * Экрны же могут открываться сворачивая предыдущий экран, а когда экран закрывается, то разворачивается предыдущий
        * в этом и вся разница
        */

    public void CreateMainScreen<TController, TView>(ControllerData paramsScreen) where TController : IControllerUI<TView>
    {
        titlePanelUIView.gameObject.SetActive(true);
        var id = ScreenId.GetMainScreen();
        if (id == null)
        {
            Debug.LogError("ВНИМАНИЕ!!! НЕТ ГЛАВНОГО ЭКРАНА В КОНФИГЕ!!!");
            return;
        }

        var controller = default(TController);
        controller = (TController)Activator.CreateInstance(typeof(TController));

        controller.SetData(paramsScreen);
        var createdAction = new Action<object>((obj) => { controller.OnCreatedView(((TView)obj)); });
        var prefab = GetPrefab(id, "[Screen]");
        var screen = Instantiate(prefab, screenRect).GetComponent<AbstractView>();
        closedActionMain = new Action(() => { controller.OnWantToCloseView((TView)(screen as object)); });
        destroyActionMain = new Action(() => { controller.OnDestoyView((TView)(screen as object)); });

        currentMainScreen = screen;
        screen.GetComponent<RectTransform>().SetSiblingIndex(-999999);
        createdAction?.Invoke(screen);

        titlePanelUIView.SetScreenConfig("Main Screen", id.Config.TitleConfig);
    }

    public void CreateScreen<TController, TView>(ScreenId id, ControllerData paramsScreen) where TController : IControllerUI<TView>
    {
        titlePanelUIView.gameObject.SetActive(true);
        CreateScreen<TController, TView>(id, GetPrefab(id, "[Screen]"), paramsScreen);
    }

    private void CreateScreen<TController, TView>(ScreenId id, GameObject screenObject, ControllerData paramsScreen) where TController : IControllerUI<TView>
    {
        var controller = default(TController);
        controller = (TController)Activator.CreateInstance(typeof(TController));

        controller.SetData(paramsScreen);
        var createdAction = new Action<object>((obj) => { controller.OnCreatedView((TView)obj); });
        var closedAction = new Action<object>((obj) => { controller.OnWantToCloseView((TView)obj); });
        var destroyAction = new Action<object>((obj) => { controller.OnDestoyView((TView)obj); });
        var screenElement = new ViewElementData { Id = id, OnClosed = closedAction, OnCreated = createdAction, OnDestroy = destroyAction };

        OpenScreen(new WindowQueue(id, screenObject, paramsScreen, screenElement));
    }

    /// <summary>
    /// Следуюшее окно
    /// </summary>
    private void OpenScreen(WindowQueue windowQueue)
    {
        var rml = stackScreen.RemoveAll(x => x == null);
        foreach (var screen in stackScreen)
        {
            screen.Hide();
        }

        if (stackScreen != null)
        {
            var prefab = windowQueue.Prafab;
            var param = windowQueue.ParamsModal;
            var screenElement = windowQueue.ScreenElement;
            var screen = Instantiate(prefab, screenRect).GetComponent<AbstractView>();
            screenElement.ViewElement = screen;

            OpenedScreens[screen] = screenElement;

            var config = screenElement.Id.Config.TitleConfig;
            titlePanelUIController.SetScreenConfig(screenElement.Id.Name, config);

            currentScreen = screen;
            screen.OnClose += OnCloseCurrentScreen;
            screen.OnWantClose += (screenObject) => { if (OpenedScreens.ContainsKey(screenObject)) { screenElement?.OnClosed?.Invoke(screenObject); } };
            screen.Init();
            screenElement.OnCreated?.Invoke(screen);
            screen.Show(false);
            stackScreen.Add(screen);
        }
    }

    /// <summary>
    /// Экран закрылся
    /// </summary>
    private void OnCloseCurrentScreen(object screenObj)
    {
        if (stackScreen != null && stackScreen.Count > 0)
        {
            var screen = (screenObj as AbstractView);
            screen.OnClose -= OnCloseCurrentScreen;
            if (OpenedScreens.ContainsKey(screenObj))
            {
                OnCloseView?.Invoke(screenObj as IViewUI, OpenedScreens[screenObj].Id);
                OnCloseScreenView?.Invoke(screenObj as IViewUI, OpenedScreens[screenObj].Id);
                OpenedScreens[screenObj].OnDestroy?.Invoke(screenObj);
                OpenedScreens.Remove(screenObj);
                if (OpenedScreens.Count > 0)
                {
                    var openedScreen = OpenedScreens.Last();
                    var config = openedScreen.Value.Id.Config.TitleConfig;
                    titlePanelUIController.SetScreenConfig(openedScreen.Value.Id.Name, config);
                }
                else
                {
                    if (currentMainScreen != null)
                    {
                        var config = ScreenId.GetMainScreen().Config.TitleConfig;
                        titlePanelUIController.SetScreenConfig("", config);
                    }
                    else
                    {
                        titlePanelUIController.SetScreenConfig("", TitlePanelConfig.GetEmpty());
                    }
                }
            }
            stackScreen.Remove(screen); //.RemoveAt(stackScreen.Count - 1);
            if (currentScreen == screen) currentScreen = null;
        }
        NextScreen();
        if (!IsAnyOpenScreen)
        {
            if (currentMainScreen != null)
            {
                var config = ScreenId.GetMainScreen().Config.TitleConfig;
                titlePanelUIController.SetScreenConfig("", config);
            }
            else
            {
                titlePanelUIController.SetScreenConfig("", TitlePanelConfig.GetEmpty());
            }
        }
    }

    /// <summary>
    /// Следующий доступный экран
    /// </summary>
    private void NextScreen()
    {
        if (stackScreen != null && stackScreen.Count > 0)
        {
            currentScreen = stackScreen[stackScreen.Count - 1];
            stackScreen[stackScreen.Count - 1].Show(true);
        }
    }

    public void CloseAllScreens()
    {
        foreach (var screen in new List<AbstractView>(stackScreen))
        {
            screen.Close();
        }
        OpenedScreens.Clear();
        OpenedWindows.Clear();
        stackScreen.Clear();
        currentScreen = null;
    }

    public void CloseAllScreensAndMain()
    {
        CloseAllScreens();
        titlePanelUIView.gameObject.SetActive(false);
        closedActionMain?.Invoke();
        if (currentMainScreen != null && currentMainScreen.gameObject != null)
        {
            Destroy(currentMainScreen.gameObject);
        }
    }

    #endregion

    #region Окна
    public void CreateWindow<TController, TView>(ScreenId id, ControllerData paramsWindow, bool showOnTop = false) where TController : IControllerUI<TView>
    {
        titlePanelUIView.gameObject.SetActive(true);
        var prefab = GetPrefab(id, "[Window]");

        var target = modalRect;
        var winGo = Instantiate(prefab, target);
        var window = winGo.GetComponent<AbstractWindow>();
        window.SetTitle(id.Name);
        var controller = (TController)Activator.CreateInstance(typeof(TController));
        controller.SetData(paramsWindow);
        var createdAction = new Action<object>((obj) => { controller.OnCreatedView((TView)obj); });
        var closedAction = new Action<object>((obj) => { controller.OnWantToCloseView((TView)obj); });
        var destroyAction = new Action<object>((obj) => { controller.OnDestoyView((TView)obj); });
        window.OnWantClose += (z) =>
        {
            if (OpenedWindows.ContainsKey(z))
            {
                closedAction?.Invoke(z);
            }
        };
        window.OnClose += (z) =>
        {
            if (OpenedWindows.ContainsKey(z))
            {
                OpenedWindows[z].OnDestroy?.Invoke(z);
                OnCloseView?.Invoke(z as IViewUI, OpenedWindows[z].Id);
                OnCloseWindowView?.Invoke(z as IViewUI, OpenedWindows[z].Id);
                OpenedWindows.Remove(z);
                if (OpenedWindows.Count > 0)
                {
                    var last = OpenedWindows.Last();
                    var config = last.Value.Id.Config.TitleConfig;
                }
                else
                {
                    if (IsAnyOpenScreen)
                    {
                        if (OpenedScreens.Count > 0)
                        {
                            var openedScreen = OpenedScreens.Last();
                            var config = openedScreen.Value.Id.Config.TitleConfig;
                        }
                    }
                    else
                    {
                        if (currentMainScreen != null)
                        {
                            var config = ScreenId.GetMainScreen().Config.TitleConfig;
                            titlePanelUIController.SetScreenConfig("", config);
                        }
                        else
                        {
                            titlePanelUIController.SetScreenConfig("", TitlePanelConfig.GetEmpty());
                        }
                    }
                }
            }
        };
        createdAction.Invoke(window);
        
        var windowElement = new ViewElementData { ViewElement = window, Id = id, OnClosed = closedAction, OnCreated = createdAction, OnDestroy = destroyAction };
        OpenedWindows[window] = windowElement;
    }

    public void CloseAllWindows()
    {
        var temp = OpenedWindows.Select(x => x).ToList();
        foreach (var pair in temp)
        {
            var window = (pair.Key as AbstractView);
            if (window != null)
            {
                window.Close();
            }
        }
        OpenedWindows.Clear();
    }

    #endregion

    public T GetWindow<T>() where T : AbstractView
    {
        var current = currentWindow ? currentWindow : (currentScreen ? currentScreen : null);
        return (T)(current != null && current.GetType() == typeof(T) ? current : default);
    }

    private void OnCloseTitleButton()
    {
        if (IsOpenModal)
        {
            if (OpenedWindows.Count > 0)
            {
                var opened = OpenedWindows.Last();
                opened.Value.OnClosed?.Invoke(opened.Key);
                //(opened.Value.Screen as AbstractWindow)?.Close();
            }
        }
        else
        {
            if (IsAnyOpenScreen)
            {
                if (OpenedScreens.Count > 0)
                {
                    var opened = OpenedScreens.Last();
                    opened.Value.OnClosed?.Invoke(opened.Key);
                    //(opened.Value.Screen as AbstractWindow)?.Close();
                }
            }
        }
    }

    private void Awake()
    {
        if (OpenedScreens == null) OpenedScreens = new Dictionary<object, ViewElementData>();
        if (OpenedWindows == null) OpenedWindows = new Dictionary<object, ViewElementData>();
        if (stackScreen == null) stackScreen = new List<AbstractView>();
        if (prefabs == null) prefabs = new List<GameObject>();

        titlePanelUIController = new TitlePanelUIController();

        titlePanelUIController.SetView(titlePanelUIView);
        titlePanelUIController.SetScreenConfig("", TitlePanelConfig.GetEmpty());
        titlePanelUIController.OnCloseButtonClick += OnCloseTitleButton;
    }
}

