public class MvpModel
{
    private Data _data;
    
    public Data Data => _data;
    
    public MvpModel(Data data)
    {
        _data = data;
    }
}