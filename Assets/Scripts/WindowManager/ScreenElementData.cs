using System;

public class ViewElementData
{
    public ScreenId Id;
    public object ViewElement;
    public Action<object> OnCreated = delegate { };
    public Action<object> OnClosed = delegate { };
    public Action<object> OnDestroy = delegate { };
}

