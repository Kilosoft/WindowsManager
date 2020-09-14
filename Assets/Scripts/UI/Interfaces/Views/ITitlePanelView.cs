using System;

public interface ITitlePanelView
{
    Action OnCloseButtonClick { get; set; }
    void SetScreenConfig(string name, TitlePanelConfig titlePanelConfig);
    void SetTitleText(string text);
}