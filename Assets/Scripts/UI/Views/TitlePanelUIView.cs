using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanelUIView : AbstractView, ITitlePanelView
{
    public Button CloseButton;
    public TMP_Text Title;
    public GameObject BackAndButtonParts;
    public GameObject BackButtonParts;
    public GameObject BackgroundParts;

    public Action OnCloseButtonClick { get; set; } = delegate { };

    private void Awake()
    {
        CloseButton.onClick.AddListener(() => OnCloseButtonClick.Invoke());
    }

    public void SetScreenConfig(string name, TitlePanelConfig titleConfig)
    {
        BackButtonParts.SetActive(titleConfig.IsShowBackButton);
        BackgroundParts.SetActive(titleConfig.IsShowBackground);

        BackAndButtonParts.SetActive(titleConfig.IsShowBackButton || titleConfig.IsShowBackground);

        Title.text = name;
    }

    public void SetTitleText(string text)
    {
        Title.text = text;
    }

    public override void UpdateUI()
    {

    }
}