using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [Header("Link to the small version")]
    public GameObject smallPaper;

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1) Check if player attempting to hide paper(right click or ctrl + click)
        bool rightClick = eventData.button == PointerEventData.InputButton.Right;
        bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
            ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

        if (rightClick || ctrlClick)
        {
            // hide enlarged version
            gameObject.SetActive(false);

            // show smaller version
            if (smallPaper != null)
            {
                smallPaper.SetActive(true);
            } else
            {
                Debug.Log("Draggable.cs: Missing link to smaller version (ignore for the email notifications)");
            }

            return;
        }


        // 2) Otherwise, we are clicking paper normally, so bring paper to FRONT
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Drag paper along with mouse
        transform.position += (Vector3)eventData.delta;
    }

}
