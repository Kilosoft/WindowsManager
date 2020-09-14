using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenUIView : AbstractView, IMainScreenUIView
{
    #region Ссылки на UI
    [SerializeField] private Button openScreen1;
    [SerializeField] private Button openScreen2;
    [SerializeField] private Button openWindow;
    #endregion

    #region Обратные вызовы
    public Action OnOpenScreen1ButtonClicked { get; set; } = delegate { };
    public Action OnOpenScreen2ButtonClicked { get; set; } = delegate { };
    public Action OnOpenWindowButtonClicked { get; set; } = delegate { };
    #endregion

    private void Start()
    {
        openScreen1.onClick.AddListener(() => OnOpenScreen1ButtonClicked.Invoke());
        openScreen2.onClick.AddListener(() => OnOpenScreen2ButtonClicked.Invoke());
        openWindow.onClick.AddListener(() => OnOpenWindowButtonClicked.Invoke());
    }

    public override void UpdateUI()
    {

    }
}
