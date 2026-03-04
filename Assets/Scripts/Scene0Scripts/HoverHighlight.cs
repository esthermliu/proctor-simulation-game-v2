using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverOverlay;

    public bool disabled = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // don't show highlight if effect is disabled
        if (disabled) return;

        // make the pencil overlay image appear
        hoverOverlay.SetActive(true);

        // make sure the pencil overlay image is on top of all other items
        // (shouldn't really be necesary since it is the only sibling of the choice)
        hoverOverlay.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // don't show highlight if effect is disabled
        if (disabled) return;

        // when NOT hovering, make pencil overlay image disappear
        hoverOverlay.SetActive(false);
    }

    public void SetDisable(bool disable)
    {
        this.disabled = disable;
    }
}
