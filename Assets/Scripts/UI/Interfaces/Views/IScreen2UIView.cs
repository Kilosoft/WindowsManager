using System;

public interface IScreen2UIView : IViewUI
{
    Action OnClickMeButtonClicked { get; set; }
    void SetCounter(int count);
}
