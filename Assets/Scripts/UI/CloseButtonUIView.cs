using UnityEngine;
using UnityEngine.UI;

public class CloseButtonUIView : MonoBehaviour
{
    public AbstractView abstractWindow;
    [SerializeField] Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            abstractWindow.WantClose();
        });
    }

    private void OnValidate()
    {
        if (closeButton == null)
        {
            closeButton = GetComponent<Button>();
        }

        if (abstractWindow == null)
        {
            abstractWindow = GetComponent<AbstractView>();
            var parrent = transform.parent;
            while (abstractWindow == null || parrent == null)
            {
                abstractWindow = parrent.GetComponent<AbstractView>();
                if (abstractWindow == null)
                {
                    parrent = parrent.parent;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
