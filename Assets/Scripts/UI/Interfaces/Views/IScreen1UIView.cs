using System;

public interface IScreen1UIView : IViewUI
{
    Action OnOpenWindowButtonClicked { get; set; }
    Action OnOpenScreen2 { get; set; }
}
