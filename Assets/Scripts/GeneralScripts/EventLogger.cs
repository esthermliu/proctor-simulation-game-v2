[System.Serializable]
public class GameEvent
{
    public string eventType;
    public string sessionId;

}

public static class EventLogger
{
    public static void Log(GameEvent gameEvent)
    {

        // Log the event to Firebase
        FirebaseProxy.LogDocument("game_events", gameEvent);
    }
}