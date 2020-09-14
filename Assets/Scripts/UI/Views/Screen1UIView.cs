using System;
using UnityEngine;
using UnityEngine.UI;

public class Screen1UIView : AbstractView, IScreen1UIView
{
    #region Ссылки на UI
    [SerializeField] private Button openWindow;
    [SerializeField] private Button openScreen2;
    #endregion

    #region Обратные вызовы
    public Action OnOpenWindowButtonClicked { get; set; } = delegate { };
    public Action OnOpenScreen2 { get; set; } = delegate { };
    #endregion

    private void Start()
    {
        openWindow.onClick.AddListener(() => OnOpenWindowButtonClicked.Invoke());
        openScreen2.onClick.AddListener(() => OnOpenScreen2.Invoke());
    }

    public override void UpdateUI()
    {

    }
}
