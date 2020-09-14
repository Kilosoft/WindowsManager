public interface IControllerUI<TView>
{
    /// <summary>
    /// Задаем дату для контролера с которой он будет работать
    /// </summary>
    /// <param name="paramsWindow"></param>
    void SetData(ControllerData data);

    /// <summary>
    /// Передаем вью которая создалась
    /// </summary>
    /// <param name="view"></param>
    void OnCreatedView(TView view);

    /// <summary>
    /// Вызываем когда окно закрылось
    /// </summary>
    /// <param name="view"></param>
    void OnWantToCloseView(TView view);

    void OnDestoyView(TView view);
}