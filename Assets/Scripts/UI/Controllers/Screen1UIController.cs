public class Screen1UIController : IScreen1UIContoller
{
    private IScreen1UIView view;

    public Screen1UIController()
    {

    }

    public void SetData(ControllerData prms)
    {
    }

    public void OnCreatedView(IScreen1UIView view)
    {
        this.view = view;
        view.OnOpenWindowButtonClicked += OpenWindow;
        view.OnOpenScreen2 += OpenScreen2;
    }

    public void OnWantToCloseView(IScreen1UIView view)
    {
        view.Close();
    }

    public void OnDestoyView(IScreen1UIView view)
    {
        this.view = null;
        view.OnOpenWindowButtonClicked -= OpenWindow;
    }

    private void OpenWindow()
    {
        UIService.OpenMessage("Хелооу, старый травник!");
    }
    
    private void OpenScreen2()
    {
        UIService.OpenScreen2();
    }
}
