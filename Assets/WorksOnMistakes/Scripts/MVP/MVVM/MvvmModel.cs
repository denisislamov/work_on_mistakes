using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class MvvmModel : INotifyPropertyChanged
{
    private int value;
    [SerializeField] private int multiplier = 1;
    private int result;
    
    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            OnPropertyChanged("Value");
        }
    }
    
    public int Multiplier
    {
        get => multiplier;
        set
        {
            multiplier = value;
            OnPropertyChanged("Multiplier");
        }
    }
    
    public int Result
    {
        get => value * multiplier;
        set
        {
            result = value;
            OnPropertyChanged("Result");
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}