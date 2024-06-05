using System;
using TMPro;
using UnityEngine;

[Serializable]
public class InputFieldDataBinder
{
    [SerializeField] private TMP_InputField inputField;
    private MvvmViewModel _viewModel;
    
    public void SetViewModel(MvvmViewModel vm)
    {
        _viewModel = vm;
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }
    
    private void OnInputValueChanged(string value)
    {
        if (int.TryParse(value, out var result))
        {
            _viewModel.Value = result;
        }
    }
    
    public void Unbind()
    {
        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
    }
}