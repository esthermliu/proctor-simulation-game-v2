using UnityEngine;
using UnityEngine.EventSystems;

public class BringGrandparentToFrontOnClick : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Ensure parent exists
        if (transform.parent == null) return;

        Transform parent = transform.parent;

        // Ensure grandparent exists
        if (parent.parent == null) return;

        Transform grandparent = parent.parent;

        // Ensure great-grandparent exists (so it has a sibling hierarchy)
        if (grandparent.parent == null) return;

        // Move the grandparent to the front within its parent
        grandparent.SetAsLastSibling();
    }
}
