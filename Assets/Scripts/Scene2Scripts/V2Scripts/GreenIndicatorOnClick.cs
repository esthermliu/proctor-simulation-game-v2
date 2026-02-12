using UnityEngine;
using UnityEngine.EventSystems;

public class GreenIndicatorOnClick : MonoBehaviour, IPointerClickHandler
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
        if (scene2Manager.ExistsOngoingEvent()) return;

        // Otherwise, there is no ongoing event, so set this as the current ongoing event
        scene2Manager.SetOngoingEvent();

        // REST OF THE BEHAVIORS

        // Hide green immediately
        student.HideIndicator();

        // Show purple + orange
        student.ShowQuestion();
        student.ShowGuide();

        // Trigger the QUESTION DOWN, then STILL animation
        studentAnimator.SetTrigger("StartQuestionDown");
        studentAnimator.SetTrigger("StartStillFromQuestionDown");

        // Also, make note that the player flagged the behavior
        // This avoids sending an email notification
        student.BehaviorClicked();
    }
}
