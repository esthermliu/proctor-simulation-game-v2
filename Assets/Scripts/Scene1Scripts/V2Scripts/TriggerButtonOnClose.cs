using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerButtonOnClose : MonoBehaviour, IPointerDownHandler
{
    public GameObject buttonToShow;

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1) Check if player attempting to hide paper(right click or ctrl + click)
        bool rightClick = eventData.button == PointerEventData.InputButton.Right;
        bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
            ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

        if (rightClick || ctrlClick)
        {
            // hide the object
            gameObject.SetActive(false);

            // show button
            buttonToShow.SetActive(true);

            return;
        }
    }

    // Function called by "close out" button on email
    public void HideEmail()
    {
        gameObject.SetActive(false);

        // show begin exam button
        buttonToShow.SetActive(true);
    }
}
