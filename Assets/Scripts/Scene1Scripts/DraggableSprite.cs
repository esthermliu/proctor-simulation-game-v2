using UnityEngine;
using TMPro; // if using TextMeshPro

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class DraggableSprite : MonoBehaviour
{
    [Header("Link to the small version")]
    public GameObject smallVersion;

    private Vector3 offset;
    private bool dragging = false;
    private SpriteRenderer sr;

    // Static counter to track top order globally
    private static int currentTopOrder = 0;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // Bring parent sprite to the front
        currentTopOrder++;
        sr.sortingOrder = currentTopOrder;

        // Also bring all child SpriteRenderers and TextMeshProRenderers to front
        foreach (var childSR in GetComponentsInChildren<SpriteRenderer>())
        {
            if (childSR != sr) // skip parent (already set)
                childSR.sortingOrder = currentTopOrder;
        }

        foreach (var text in GetComponentsInChildren<TextMeshPro>())
        {
            text.sortingOrder = currentTopOrder;
        }

        // Hide logic for right click / Ctrl+Click
        if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))))
        {
            gameObject.SetActive(false);

            if (smallVersion != null)
                smallVersion.SetActive(true);

            return;
        }

        // Left click: start dragging
        if (Input.GetMouseButton(0))
        {
            dragging = true;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            offset = transform.position - mouseWorld;
        }
    }

    void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            transform.position = mouseWorld + offset;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
    }
}
