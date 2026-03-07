using UnityEngine;
using UnityEngine.EventSystems;

public class RedIndicatorOnClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    [Header("Cursor")]
    public Texture2D hoverCursor;
    public Vector2 hotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    [Header("Audio Source (optional)")]
    public AudioSource audioSource;

    private Student student;
    private Animator studentAnimator;

    void Start()
    {
        student = GetComponentInParent<Student>();

        // Get the Animator component from the student
        if (student != null)
        {
            studentAnimator = student.GetComponent<Animator>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Only show hover cursor if there is NO ongoing event
        if (!scene2Manager.ExistsOngoingEvent())
        {
            Cursor.SetCursor(hoverCursor, hotspot, cursorMode);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Reset cursor immediately on click
        Cursor.SetCursor(null, Vector2.zero, cursorMode);

        // Do NOT process the event if there is already an ongoing event
        if (scene2Manager.ExistsOngoingEvent()) {
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.bad_behavior_clicked_during_ongoing_event,
                studentName = student.name,
                elapsedTime = scene2Manager.ElapsedTime,
            });
            return;
        }

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.bad_behavior_clicked,
            studentName = student.name,
            elapsedTime = scene2Manager.ElapsedTime,
        });

        // Otherwise, there is no ongoing event, so set this as the current ongoing event
        scene2Manager.SetOngoingEvent();

        // AUDIO: since this is the ongoing event, play the click sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Increment number of flagged students
        if (GameManager.Instance != null)
        {
            GameManager.Instance.FlagStudent();
        }

        // Hide red indicator
        student.HideIndicator();

        // Show yellow consequence
        student.ShowInvestigate();

        // Trigger the STILL animation
        studentAnimator.SetTrigger("StartStill");

        // Also, make note that the player flagged the behavior
        // This avoids sending an email notification
        student.BehaviorClicked();

    }
}
