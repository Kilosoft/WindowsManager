public class Screen2UIController : IScreen2UIContoller
{
    private IScreen2UIView view;

    private int countClicked;

    public Screen2UIController()
    {
        
    }

    public void SetData(ControllerData prms)
    {
        countClicked = prms.GetParam("CountClicked", 0);
    }

    public void OnCreatedView(IScreen2UIView view)
    {
        this.view = view;
        view.OnClickMeButtonClicked += ClickMeButton;
        view.SetCounter(countClicked);
    }

    public void OnWantToCloseView(IScreen2UIView view)
    {
        view.Close();
    }

    public void OnDestoyView(IScreen2UIView view)
    {
        this.view = null;
        view.OnClickMeButtonClicked -= ClickMeButton;
    }

    private void ClickMeButton()
    {
        countClicked++;
        view.SetCounter(countClicked);
    }
}
