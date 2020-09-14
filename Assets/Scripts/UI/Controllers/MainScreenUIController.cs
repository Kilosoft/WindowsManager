public class MainScreenUIController : IMainScreenUIController
{
    private IMainScreenUIView view;

    public MainScreenUIController()
    {

    }

    public void SetData(ControllerData prms)
    {
    }

    public void OnCreatedView(IMainScreenUIView view)
    {
        this.view = view;
        view.OnOpenScreen1ButtonClicked += OpenScreen1;
        view.OnOpenScreen2ButtonClicked += OpenScreen2;
        view.OnOpenWindowButtonClicked += OpenWindow;
    }

    public void OnWantToCloseView(IMainScreenUIView view)
    {
        view.Close();
    }

    public void OnDestoyView(IMainScreenUIView view)
    {
        this.view = null;
        view.OnOpenScreen1ButtonClicked -= OpenScreen1;
        view.OnOpenScreen2ButtonClicked -= OpenScreen2;
        view.OnOpenWindowButtonClicked -= OpenWindow;
    }

    private void OpenScreen1()
    {
        UIService.OpenScreen1();
    }

    private void OpenScreen2()
    {
        UIService.OpenScreen2();
    }

    private void OpenWindow()
    {
        UIService.OpenWindow();
    }
}
