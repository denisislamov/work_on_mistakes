using System.ComponentModel;
using TMPro;
using UnityEngine;

public class MvvmView : MonoBehaviour
{
    [SerializeField] private TMP_Text result;
    [SerializeField] private InputFieldDataBinder inputFieldDataBinder;

    [Space(10)]
    private MvvmViewModel _viewModel;
    
    public void SetViewModel(MvvmViewModel viewModel)
    {
        _viewModel = viewModel;
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
        inputFieldDataBinder.SetViewModel(viewModel);
    }
    
    private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is "Value" or "Multiplier")
        {
            result.text = _viewModel.Result.ToString();
        }
    }

    private void OnDestroy()
    {
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}