using System;

public interface ITitlePanelUIController
{
    Action OnCloseButtonClick { get; set; }
    void SetView(ITitlePanelView view);
    void SetScreenConfig(string title, TitlePanelConfig titlePanelConfig);
}