using System;

public interface IMainScreenUIView : IViewUI
{
    Action OnOpenScreen1ButtonClicked { get; set; }
    Action OnOpenScreen2ButtonClicked { get; set; }
    Action OnOpenWindowButtonClicked { get; set; }
}
