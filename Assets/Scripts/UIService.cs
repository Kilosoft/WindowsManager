using UnityEngine;

public static class UIService
{
    private static WindowManager windowManagerCached;
    private static WindowManager windowManager
    {
        get
        {
            if (windowManagerCached == null)
            {
                windowManagerCached = Object.FindObjectOfType<WindowManager>();
            }
            return windowManagerCached;
        }
    }

    public static void OpenMainScreen()
    {
        var data = new ControllerData();
        windowManager.CreateMainScreen<MainScreenUIController, IMainScreenUIView>(data);
    }

    public static void OpenScreen1()
    {
        var data = new ControllerData();
        windowManager.CreateScreen<Screen1UIController, IScreen1UIView>(ScreenId.Screen1, data);
    }

    public static void OpenScreen2()
    {
        var data = new ControllerData();
        windowManager.CreateScreen<Screen2UIController, IScreen2UIView>(ScreenId.Screen2, data);
    }

    public static void OpenWindow()
    {
        var data = new ControllerData();
        windowManager.CreateWindow<WindowUIController, IWindowUIView>(ScreenId.Window, data);
    }

    public static void OpenMessage(string text)
    {
        var data = new ControllerData();
        data.SetParam("Text", text);
        windowManager.CreateWindow<WindowMessageUIController, IWindowMessageUIView>(ScreenId.MessageWindow, data);
    }
}
