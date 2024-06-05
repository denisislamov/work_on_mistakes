public class MvpPresenter
{
    private MvpModel _model;
    private MvpView _view;

    public MvpPresenter(MvpModel model, MvpView view)
    {
        _model = model;
        _view = view;
        
        _view.RegisterButtonListener(OnButtonClicked);
        _view.RegisterInputListener(OnInputValueChanged);
    }

    private void OnButtonClicked()
    {
        var result = _model.Data.Value * _model.Data.Multiplier;
        _view.UpdateText(result);
    }
    
    private void OnInputValueChanged(string value)
    {
        if (int.TryParse(value, out var intValue))
        {
            _model.Data.Value = intValue;
        }
    }
    
    public void UnregisterListeners()
    {
        _view.UnregisterButtonListener(OnButtonClicked);
        _view.UnregisterInputListener(OnInputValueChanged);
    }
}