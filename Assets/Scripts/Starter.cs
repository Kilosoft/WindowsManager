using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private WindowManager windowManager;
    void Start()
    {
        UIService.OpenMainScreen();
    }
}
