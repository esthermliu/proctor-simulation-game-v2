using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public double gameVersion = Constants.GAME_VERSION; 
    public string subversion = GameManager.Instance?.subversion; // for A/B testing
    public string hostingPlatform = Constants.HOSTING_PLATFORM;

    [SerializeField]
    private string eventType;

    public EventType eventTypeEnum
    {
        get => eventTypeEnum;
        set { eventType = value.ToString(); }
    }
    public string sessionId = GameManager.Instance?.sessionId;
    public string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    public string description = null;
    public int index = 0;
    public GameState gameState = null;
}

public enum EventType
{
    session_start,
    controls_page_entered,
    credits_page_entered,
    scene_entered,
    item_clicked,
    orientation_skipped,
    admission_decision_correct,
    admission_decision_incorrect,
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