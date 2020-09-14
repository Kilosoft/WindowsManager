using System;

public class TitlePanelUIController : ITitlePanelUIController
{
    public Action OnCloseButtonClick { get; set; } = delegate { };

    private ITitlePanelView view;

    public TitlePanelUIController()
    {
    }

    public void SetView(ITitlePanelView view)
    {
        this.view = view;
        view.OnCloseButtonClick += () => OnCloseButtonClick?.Invoke();
    }

    public void SetScreenConfig(string name, TitlePanelConfig titlePanelConfig)
    {
        view?.SetScreenConfig(name, titlePanelConfig);
    }
}
