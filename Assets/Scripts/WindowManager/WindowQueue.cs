using UnityEngine;

[System.Serializable]
public class WindowQueue
{
    public ScreenId Id { get; }
    public GameObject Prafab { get; }
    public ControllerData ParamsModal { get; }
    public ViewElementData ScreenElement { get; }

    public WindowQueue(ScreenId id, GameObject prefab, ControllerData paramsModal, ViewElementData screenElement)
    {
        Id = id;
        Prafab = prefab;
        ParamsModal = paramsModal;
        ScreenElement = screenElement;
    }
}