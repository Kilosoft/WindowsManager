public class WindowMessageUIController : IWindowMessageUIContoller
{
    private IWindowMessageUIView view;

    private string setText;

    public WindowMessageUIController()
    {
        
    }

    public void SetData(ControllerData prms)
    {
        setText = prms.GetParam("Text", "");
    }

    public void OnCreatedView(IWindowMessageUIView view)
    {
        this.view = view;
        view.SetText(setText);
    }

    public void OnWantToCloseView(IWindowMessageUIView view)
    {
        view.Close();
    }

    public void OnDestoyView(IWindowMessageUIView view)
    {
        this.view = null;
    }
}