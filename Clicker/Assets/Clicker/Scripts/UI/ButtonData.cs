namespace Clicker.Scripts.UI
{
    public class ButtonData
    {
        public readonly string Info;
        public readonly bool IsEnabled;

        public ButtonData(string info, bool isEnabled)
        {
            Info = info;
            IsEnabled = isEnabled;
        }
    }
}