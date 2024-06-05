public class MvcView : View
{
    public void UpdateText(MvcModel model)
    {
        text.text = model.Data.Result.ToString();
    }
}