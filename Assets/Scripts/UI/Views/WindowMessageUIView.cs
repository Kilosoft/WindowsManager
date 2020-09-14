using TMPro;

public class WindowMessageUIView : AbstractWindow, IWindowMessageUIView
{
    #region Ссылки на UI
    public TMP_Text message;
    #endregion

    #region Обратные вызовы
    #endregion

    public void SetText(string text)
    {
        message.text = text;
    }

    public override void UpdateUI()
    {

    }
}

