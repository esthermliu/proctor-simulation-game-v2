using UnityEngine;
using TMPro; // if using TextMeshPro

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class ClickableID : MonoBehaviour
{
    public Color hoverColor = Color.yellow;      // color when hovered
    public GameObject enlargedID;                // assign enlarged version in Inspector

    [Header("Sorting Order Manager")]
    public SortingOrderManager sortingOrderManager;

    private SpriteRenderer sr;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        //// Initially hide the small ID, but keep collider active
        //sr.enabled = false;

        if (enlargedID != null)
            enlargedID.SetActive(false); // make sure enlarged is hidden at start
    }

    void OnMouseEnter()
    {

        Debug.Log("HOVER!");
        sr.color = hoverColor; // change color when mouse hovers
    }

    void OnMouseExit()
    {
        sr.color = originalColor; // revert when mouse leaves
    }

    void OnMouseDown()
    {
        if (enlargedID != null)
        {
            // hide this small ID
            gameObject.SetActive(false);

            // show the enlarged version
            enlargedID.SetActive(true);

            // --- Bring the enlargedID to the front ---
            int largestSortingOrder = sortingOrderManager.getLargestSortingOrder();

            // Get SpriteRenderer of enlargedID (parent)
            SpriteRenderer parentSR = enlargedID.GetComponent<SpriteRenderer>();
            if (parentSR != null)
            {
                parentSR.sortingOrder = largestSortingOrder + 1;
            }

            // Handle children SpriteRenderers
            int maxChildOffset = 0;
            int childOffset = 1;

            foreach (var childSR in enlargedID.GetComponentsInChildren<SpriteRenderer>())
            {
                if (childSR != parentSR)
                {
                    childSR.sortingOrder = parentSR.sortingOrder + childOffset;
                    if (childOffset > maxChildOffset)
                        maxChildOffset = childOffset;
                }
            }

            // Handle TextMeshPro children
            foreach (var text in enlargedID.GetComponentsInChildren<TextMeshPro>())
            {
                text.sortingOrder = parentSR.sortingOrder + childOffset;
                if (childOffset > maxChildOffset)
                    maxChildOffset = childOffset;
            }

            // Update the global top order
            sortingOrderManager.setLargestSortingOrder(parentSR.sortingOrder + maxChildOffset);

            //// TODO: enlarged ID should always be on top
            //int largestSortingOrder = sortingOrderManager.getLargestSortingOrder();
            //sr.sortingOrder = largestSortingOrder + 1;

            //// TODO: make sure that the children of the component have higher
            //// sorting orders
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
            //largestSortingOrder = sr.sortingOrder + maxChildOffset;
            //sortingOrderManager.setLargestSortingOrder(largestSortingOrder);
        }
    }
}
