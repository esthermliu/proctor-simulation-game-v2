using UnityEngine;
using UnityEngine.EventSystems;

public class BringParentToFrontOnClick : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Make sure this object has a parent
        if (transform.parent == null) return;

        Transform parent = transform.parent;

        // Make sure the parent has a parent (grandparent exists)
        if (parent.parent == null) return;

        // make sure grandparent's parent exists
        if (parent.parent.parent == null) return;

        // Move the parent to the front within the grandparent
        parent.SetAsLastSibling();
    }
}
