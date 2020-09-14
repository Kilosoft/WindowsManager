using System;
using UnityEngine;

/// <summary>
/// Окошки
/// </summary>
public abstract class AbstractView : MonoBehaviour, IViewUI
{
    public virtual void Init() { }
    public abstract void UpdateUI();

    private bool isClosed;

    private Canvas _canvas;
    public Canvas CanvasForMe
    {
        get
        {
            if (_canvas == null)
            {
                Canvas[] c = GetComponentsInParent<Canvas>();
                _canvas = c[c.Length - 1];
            }
            return _canvas;
        }
    }

    /// <summary>
    /// Показать окошко
    /// </summary>
    public virtual void Show(bool updateUI)
    {
        gameObject.SetActive(true);
        if (updateUI) UpdateUI();
        OnShow();
    }
    /// <summary>
    /// Спрятать окошко
    /// </summary>
    public virtual void Hide()
    {
        OnHide(this);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Закрытие окна
    /// </summary>
    public virtual void Close()
    {
        if (!isClosed)
        {
            isClosed = true;
            OnClose(this);
            Destroy(gameObject);
        }
    }

    public virtual void WantClose()
    {
        OnWantClose(this);
    }

    /// <summary>
    /// Обартный вызов на показать
    /// </summary>
    public Action OnShow = delegate { };
    /// <summary>
    /// Обартный вызов на скрыть
    /// </summary>
    public Action<object> OnHide = delegate { };
    /// <summary>
    /// Обратный вызов когда хотим закрыть окно
    /// </summary>
    public Action<object> OnWantClose = delegate { };
    /// <summary>
    /// Обратный вызов что окно/экран закрывается
    /// </summary>
    public Action<object> OnClose = delegate { };
}