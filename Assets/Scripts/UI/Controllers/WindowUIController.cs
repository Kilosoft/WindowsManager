public class WindowUIController : IWindowUIContoller
{
    private IWindowUIView view;

    public WindowUIController()
    {
        
    }

    public void SetData(ControllerData prms)
    {
        
    }

    public void OnCreatedView(IWindowUIView view)
    {
        this.view = view;
    }

    public void OnWantToCloseView(IWindowUIView view)
    {
        view.Close();
    }

    public void OnDestoyView(IWindowUIView view)
    {
        this.view = null;
    }
}