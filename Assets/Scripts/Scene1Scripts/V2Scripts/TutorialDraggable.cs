using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialDraggable : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [Header("Link to the small version")]
    public GameObject smallPaper;

    [Header("Link to Supervisor Pause 1 Manager (if ID card)")]
    public SupervisorPause1Manager supervisorPause1Manager;

    [Header("Link to Supervisor Pause 2 Manager (ONLY DAY 2)")]
    public SupervisorPause2Day2Manager supervisorPause2Day2Manager;

    [Header("Right-Clickable?")]
    public bool rightClickable = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (rightClickable) {

            // 1) Check if player attempting to hide paper(right click or ctrl + click)
            bool rightClick = eventData.button == PointerEventData.InputButton.Right;
            bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
                ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

            if (rightClick || ctrlClick)
            {
                if (supervisorPause1Manager != null)
                {
                    // move the tutorial along
                    supervisorPause1Manager.CompletePause1();
                }

                if (supervisorPause2Day2Manager != null)
                {
                    // move the tutorial along once we have right clicked the external ticket
                    supervisorPause2Day2Manager.CompletePause2();
                }

                // hide enlarged version
                gameObject.SetActive(false);

                // show smaller version
                if (smallPaper != null)
                {
                    smallPaper.SetActive(true);
                }
                else
                {
                    Debug.Log("Draggable.cs: Missing link to smaller version (ignore for the email notifications)");
                }

                return;
            }
        }
    
        // 2) Otherwise, we are clicking paper normally, so bring paper to FRONT
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Drag paper along with mouse
        transform.position += (Vector3)eventData.delta;
    }

    // enable right click once the tutorial on the item is complete
    public void SetRightClickable(bool rightClickable)
    {
        this.rightClickable = rightClickable;
    }

}
