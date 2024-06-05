using System.ComponentModel;
using System.Runtime.CompilerServices;

public class MvvmViewModel : INotifyPropertyChanged
{
    private MvvmModel model;
 
    public MvvmViewModel(MvvmModel p)
    {
        model = p;
    }
    
    public int Value
    {
        get { return model.Value; }
        set
        {
            model.Value = value;
            OnPropertyChanged("Value");
        }
    }
    
    public int Multiplier => model.Multiplier;

    public int Result => Value * Multiplier;

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}