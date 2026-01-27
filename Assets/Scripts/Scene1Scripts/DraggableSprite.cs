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

    [Header("Sorting Order Manager")]
    public SortingOrderManager sortingOrderManager;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // set parent to 1 more than the highest sorting order so far
        sr.sortingOrder = sortingOrderManager.getLargestSortingOrder() + 1;

        // Determine maximum child offset
        int maxChildOffset = 0;

        // Bring all children slightly above parent
        int childOffset = 1; // spacing between parent and children
        foreach (var childSR in GetComponentsInChildren<SpriteRenderer>())
        {
            Debug.Log("HI THERE IS CHILDREN??");
            if (childSR != sr)
            {
                Debug.Log("HELLO THIS IS THE CHILDSR " + childSR);
                childSR.sortingOrder = sr.sortingOrder + childOffset;
                if (childOffset > maxChildOffset)
                    maxChildOffset = childOffset;
            }
        }

        foreach (var text in GetComponentsInChildren<TextMeshPro>())
        {
            text.sortingOrder = sr.sortingOrder + childOffset;
            if (childOffset > maxChildOffset)
                maxChildOffset = childOffset;
        }

        // Update global top order to reflect the true topmost sortingOrder
        sortingOrderManager.setLargestSortingOrder(sr.sortingOrder + maxChildOffset);



        //// Increment global top order for parent
        //currentTopOrder++;

        //// Parent gets the base order
        //sr.sortingOrder = currentTopOrder;

        //// Determine maximum child offset
        //int maxChildOffset = 0;

        //// Bring all children slightly above parent
        //int childOffset = 1; // spacing between parent and children
        //foreach (var childSR in GetComponentsInChildren<SpriteRenderer>())
        //{
        //    if (childSR != sr)
        //    {
        //        childSR.sortingOrder = sr.sortingOrder + childOffset;
        //        if (childOffset > maxChildOffset)
        //            maxChildOffset = childOffset;
        //    }
        //}

        //foreach (var text in GetComponentsInChildren<TextMeshPro>())
        //{
        //    text.sortingOrder = sr.sortingOrder + childOffset;
        //    if (childOffset > maxChildOffset)
        //        maxChildOffset = childOffset;
        //}

        //// Update global top order to reflect the true topmost sortingOrder
        //currentTopOrder = sr.sortingOrder + maxChildOffset;



        //----------------

        //// Bring parent sprite to the front
        //currentTopOrder++;
        //sr.sortingOrder = currentTopOrder;

        //// Also bring all child SpriteRenderers and TextMeshProRenderers to front
        //foreach (var childSR in GetComponentsInChildren<SpriteRenderer>())
        //{
        //    if (childSR != sr) // skip parent (already set)
        //        childSR.sortingOrder = currentTopOrder;
        //}

        //foreach (var text in GetComponentsInChildren<TextMeshPro>())
        //{
        //    text.sortingOrder = currentTopOrder;
        //}

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
