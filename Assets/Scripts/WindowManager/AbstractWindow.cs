using TMPro;
using UnityEngine;

public abstract class AbstractWindow : AbstractView
{
    [SerializeField] private TMP_Text title;
    public void SetTitle(string text)
    {
        title.text = text;
    }
}
