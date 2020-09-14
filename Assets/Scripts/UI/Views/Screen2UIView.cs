using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen2UIView : AbstractView, IScreen2UIView
{
    #region Ссылки на UI
    [SerializeField] private Button clickMeButton;
    [SerializeField] private TMP_Text counterText;
    #endregion

    #region Обратные вызовы
    public Action OnClickMeButtonClicked { get; set; } = delegate { };
    #endregion

    private void Start()
    {
        clickMeButton.onClick.AddListener(() => OnClickMeButtonClicked.Invoke());
    }

    public void SetCounter(int count)
    {
        counterText.text = $"Clicked: {count}";
    }

    public override void UpdateUI()
    {

    }
}
