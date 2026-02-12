using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickHide : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // 1) Check if player attempting to hide paper(right click or ctrl + click)
        bool rightClick = eventData.button == PointerEventData.InputButton.Right;
        bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
            ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

        if (rightClick || ctrlClick)
        {
            // hide object
            gameObject.SetActive(false);

            return;
        }
    }
}
