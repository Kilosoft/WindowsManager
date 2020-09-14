using System.Linq;

public class ScreenId : StringEnum
{
    #region Экраны
    public static ScreenId MainScreen = new ConfigScreenId("MainScreenUI", "Супер экран", ScreenConfig.GetMainScreen());
    public static ScreenId Screen1 = new ConfigScreenId("ScreenUI1", "Экран 1", ScreenConfig.GetNormalScreen());
    public static ScreenId Screen2 = new ConfigScreenId("ScreenUI2", "Экран 2", ScreenConfig.GetNormalScreen());
    public static ScreenId Screen3 = new ConfigScreenId("ScreenUI2", "Экран 2", ScreenConfig.GetNormalScreen());
    #endregion

    #region Окна
    public static ScreenId Window = new ConfigScreenId("WindowUI", "Стих!", ScreenConfig.GetNormalWindow());
    public static ScreenId MessageWindow = new ConfigScreenId("MessageWindowUI", "Информация", ScreenConfig.GetNormalWindow());
    #endregion


    public int UsagesCount = 0;
    public string Name;
    public ScreenConfig Config;

    public ScreenId(string val) : base(val)
    {
    }

    public static ScreenId GetMainScreen()
    {
        return GetAll(typeof(ConfigScreenId)).Select(x => x as ScreenId).FirstOrDefault(x => x.Config.IsMainScreen);
    }
}