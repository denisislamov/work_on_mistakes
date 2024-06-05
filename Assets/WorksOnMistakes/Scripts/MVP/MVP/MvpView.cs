public class MvpView : View
{
    public void UpdateText(int result)
    {
        text.text = result.ToString();
    }
}