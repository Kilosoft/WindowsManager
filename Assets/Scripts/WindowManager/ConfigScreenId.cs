public class ConfigScreenId : ScreenId
{
    public ConfigScreenId(string val, string name, ScreenConfig config) : base(val)
    {
        Name = name;
        Config = config;
    }
}

public enum ScreenTypeEnum
{
    None = 0,
    Screen = 1,
    Window = 2,
}

public class TitlePanelConfig
{
    public bool IsShowBackButton { get; private set; }
    public bool IsShowBackground { get; private set; }
    public static TitlePanelConfig Get(bool isShowBackButton, bool isShowBackground)
    {
        return new TitlePanelConfig
        {
            IsShowBackButton = isShowBackButton,
            IsShowBackground = isShowBackground
        };
    }

    public static TitlePanelConfig GetEmpty() => Get(false, false);
    public static TitlePanelConfig GetFromMainSceen() => Get(false, false);
    public static TitlePanelConfig GetFromNormalScreen() => Get(true, true);
    public static TitlePanelConfig GetFromNormalWindow() => Get(false, false);
    public static TitlePanelConfig GetBacButtonWithoutBackground() => Get(true, false);
}

public class ScreenConfig
{
    public TitlePanelConfig TitleConfig;
    public ScreenTypeEnum ScreenType;
    public bool IsMainScreen;
    public bool ShowOnTop;

    public static ScreenConfig GetConfig(TitlePanelConfig titleConfig, ScreenTypeEnum screenType, bool isMainScreen, bool isShowOnTop = false)
    {
        return new ScreenConfig
        {
            TitleConfig = titleConfig,
            ScreenType = screenType,
            IsMainScreen = isMainScreen,
            ShowOnTop = isShowOnTop
        };
    }
    public static ScreenConfig GetMainScreen() => GetConfig(TitlePanelConfig.GetFromMainSceen(), ScreenTypeEnum.Screen, true);
    public static ScreenConfig GetNormalScreen() => GetConfig(TitlePanelConfig.GetFromNormalScreen(), ScreenTypeEnum.Screen, false);
    public static ScreenConfig GetNormalWindow() => GetConfig(TitlePanelConfig.GetFromNormalWindow(), ScreenTypeEnum.Window, false);
    public static ScreenConfig GetShowOnTopWindow() => GetConfig(TitlePanelConfig.GetFromNormalWindow(), ScreenTypeEnum.Window, false, true);
    public static ScreenConfig GetScreenWithoutBackground() => GetConfig(TitlePanelConfig.GetBacButtonWithoutBackground(), ScreenTypeEnum.Screen, false);
    public static ScreenConfig GetScreenWithoutTopPanel() => GetConfig(TitlePanelConfig.GetEmpty(), ScreenTypeEnum.Screen, false);
}
