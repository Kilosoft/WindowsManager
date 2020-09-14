using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EscapeEventButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.onClick.Invoke();
        }
    }
}
