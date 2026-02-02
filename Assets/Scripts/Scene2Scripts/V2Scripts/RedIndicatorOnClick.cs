using UnityEngine;
using UnityEngine.EventSystems;

public class RedIndicatorOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    private Student student;

    void Start()
    {
        student = GetComponentInParent<Student>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Do NOT process the event if there is already an ongoing event
        if (scene2Manager.ExistsOngoingEvent()) return;

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
    }
}
