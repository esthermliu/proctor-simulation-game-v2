using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverOverlay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // make the pencil overlay image appear
        hoverOverlay.SetActive(true);

        // make sure the pencil overlay image is on top of all other items
        // (shouldn't really be necesary since it is the only sibling of the choice)
        hoverOverlay.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // when NOT hovering, make pencil overlay image disappear
        hoverOverlay.SetActive(false);
    }
}
