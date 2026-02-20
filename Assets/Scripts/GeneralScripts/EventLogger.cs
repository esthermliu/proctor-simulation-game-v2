using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public double gameVersion = Constants.GAME_VERSION; 
    public string subversion; // for A/B testing
    public string hostingPlatform = Constants.HOSTING_PLATFORM;

    [SerializeField]
    private string eventType;

    public EventType eventTypeEnum
    {
        get => eventTypeEnum;
        set { eventType = value.ToString(); }
    }
    public string sessionId;

    public string sceneName = null;
}

public enum EventType
{
    session_start,
    controls_page_entered,
    credits_page_entered,
    scene_entered,
}


public static class EventLogger
{
    public static void Log(GameEvent gameEvent)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR

        // Log the event to Firebase
        FirebaseProxy.LogDocument("game_events", gameEvent);

        #endif
    }
}