using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialItem : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [Header("Link to the small version")]
    public GameObject smallPaper;

    [SerializeField] private SupervisorSpeechManager speechManager;

    // Cannot click on the ID until we allow the player to (during the pauses, not during dialogue)
    private bool disabled = false;

    // Pause 1 fields
    private bool pause1 = true;

    // Pause 2 fields


    // IDEA IS TO FORCE PLAYERS TO LEARN RIGHT CLICK MECHANISM during pause 1
    public void OnPointerClick(PointerEventData eventData)
    {
        // DO NOT ALLOW CLICK if disabled is true
        if (disabled) return;

        // 1) Check if player attempting to hide paper(right click or ctrl + click)
        bool rightClick = eventData.button == PointerEventData.InputButton.Right;
        bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
            ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

        if (rightClick || ctrlClick)
        {
            if (pause1)
            {
                FinishInspection();
            }

            // hide enlarged version
            gameObject.SetActive(false);

            // show smaller version
            if (smallPaper != null)
            {
                smallPaper.SetActive(true);
            }

            return;
        }


        // 2) Otherwise, we are clicking paper normally, so bring paper to FRONT
        transform.SetAsLastSibling();
    }

    private void FinishInspection()
    {
        // we are done with pause 1 now
        pause1 = false;

        // Pause 1 is over, so disable this ID for now
        disabled = true;

        // continue with dialogue
        speechManager.ResumeDialogue();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Drag paper along with mouse
        transform.position += (Vector3)eventData.delta;
    }

    // Called during the pauses
    public void EnableID()
    {
        disabled = false;
    }

    // Called when the dialogue continues
    public void DisableID()
    {
        disabled = true;
    }

}
