public class MvcController
{
    private MvcModel _model;
    private MvcView _view;

    public MvcController(MvcModel model, MvcView view)
    {
        _model = model;
        _view = view;
        
        _view.RegisterButtonListener(OnButtonClicked);
        _view.RegisterInputListener(OnInputValueChanged);
    }

    private void OnButtonClicked()
    {
        var result = _model.Data.Value * _model.Data.Multiplier;
        _model.Data.Result = result;
        
        _view.UpdateText(_model);
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