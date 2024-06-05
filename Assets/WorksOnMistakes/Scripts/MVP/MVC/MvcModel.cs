public class MvcModel
{
    private MvcData _data;
    public MvcData Data => _data;
    
    public MvcModel(MvcData data)
    {
        _data = data;
    }
}