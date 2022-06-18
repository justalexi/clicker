namespace Events
{
    public delegate void GameEventDelegate(GameEventType type, object data);

    public class GameEvent
    {
        public static event GameEventDelegate OnGameEvent;

        public static void Trigger(GameEventType type, object data = null)
        {
            OnGameEvent?.Invoke(type, data);
        }
    }
}