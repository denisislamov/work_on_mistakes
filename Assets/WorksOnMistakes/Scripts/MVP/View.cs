using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_InputField _input;
    
    public event Action OnButtonClicked = () => { };
    public event Action<string> OnInputValueChanged = value => { };
    
    private void OnEnable()
    {
        _button.onClick.AddListener(() =>
        {
            OnButtonClicked?.Invoke();
        });
        
        _input.onValueChanged.AddListener(value =>
        {
            OnInputValueChanged?.Invoke(value);
        });
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked.Invoke);
        _input.onValueChanged.RemoveListener(OnInputValueChanged.Invoke);
    }
    
    public void RegisterButtonListener(Action action)
    {
        OnButtonClicked += action;
    }
    
    public void UnregisterButtonListener(Action action)
    {
        OnButtonClicked -= action;
    }
    
    public void RegisterInputListener(Action<string> action)
    {
        OnInputValueChanged += action;
    }
    
    public void UnregisterInputListener(Action<string> action)
    {
        OnInputValueChanged -= action;
    }
}