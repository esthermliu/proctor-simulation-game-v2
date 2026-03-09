using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public double gameVersion = Constants.GAME_VERSION; 
    public string subversion = GameManager.Instance?.subversion; // for A/B testing

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
    public bool isCorrect = false;
    public string studentName = null;
    public int index = 0;
    public float elapsedTime = 0f; // for scene 2 only
    public GameState gameState = GameManager.Instance?.state;
}

public enum EventType
{
    session_start,
    controls_page_entered,
    credits_page_entered,
    scene_entered,
    item_clicked,
    pause_completed,
    orientation_skipped,
    day_started,
    admission_decision,
    exam_started,
    bad_behavior_clicked,
    bad_behavior_clicked_during_ongoing_event,
    bad_behavior_missed,
    question_clicked,
    question_clicked_during_ongoing_event,
    question_missed,
    question_answered,
    investigation_initiated,
    investigation_declined,
    help_clicked,
    scroll_clicked,
    report_decision,
    supervisor_talk_clicked,
    eval_signed,
    ending_determined,
}


public static class EventLogger
{
    public static void Log(GameEvent gameEvent)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR

        // Log the event to Firebase
        FirebaseProxy.LogDocument("game_events_2", gameEvent);

        #else

        // Log the event to the console
        Debug.Log("Analytics Event: " + JsonUtility.ToJson(gameEvent));

        #endif
    }
}