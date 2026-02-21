using UnityEngine;
using UnityEngine.EventSystems;

public class RedIndicatorOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        // Do NOT process the event if there is already an ongoing event
        if (scene2Manager.ExistsOngoingEvent()) {
            EventLogger.Log(new GameEvent {
                eventTypeEnum = EventType.bad_behavior_clicked_during_ongoing_event,
                elapsedTime = scene2Manager.ElapsedTime,
            });
            return;
        }

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.bad_behavior_clicked,
            elapsedTime = scene2Manager.ElapsedTime,
        });

        // Otherwise, there is no ongoing event, so set this as the current ongoing event
        scene2Manager.SetOngoingEvent();

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
