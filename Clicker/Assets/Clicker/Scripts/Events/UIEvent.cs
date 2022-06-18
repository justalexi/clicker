namespace Events
{
    public delegate void UIEventDelegate(UIEventType type, object data);

    public class UIEvent
    {
        public static event UIEventDelegate OnUIEvent;

        public static void Trigger(UIEventType type, object data = null)
        {
            OnUIEvent?.Invoke(type, data);
        }
    }
}