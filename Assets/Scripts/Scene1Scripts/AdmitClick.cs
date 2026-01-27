using UnityEngine;

public class AdmitClick : MonoBehaviour
{
    public ReviewDecisionController controller;
    public GameObject admitRedCheckmark;
    public GameObject denyRedCheckmark;

    public SortingOrderManager sortingOrderManager;

    // 0 = no decision, 1 is YES admit, 2 is NO admit
    private int admit;

    void Start()
    {
        admit = 0;
    }

    // Use this function to determine which action to take when
    // player clicks "complete review"
    public int getAdmit()
    {
        return admit;
    }

    void OnMouseDown()
    {
        // When the player clicks the admit checkbox
        // Case 1: the deny checkbox is already checked
        //   --> 1a) The admit checkbox must NOT already be checked
        //            --> we check the admit checkbox AND uncheck the deny checkbox
        // Case 2: the deny checkbox is NOT already checked
        //   --> 1a) The admit checkbox is NOT already checked
        //          --> we check the admit checkbox
        //   --> 1b) The admit checkbox IS already checked
        //          --> we uncheck the admit checkbox
        Debug.Log("ADMIT ZONE CLICKED");


        if (denyRedCheckmark.activeSelf)
        {
            denyRedCheckmark.SetActive(false);
            admitRedCheckmark.SetActive(true);

            // TODO: refactor code
            // make sure that the red checkmark is on top
            SpriteRenderer checkmarkSR = admitRedCheckmark.GetComponent<SpriteRenderer>();
            if (checkmarkSR != null)
            {
                int topOrder = sortingOrderManager.getLargestSortingOrder();
                checkmarkSR.sortingOrder = topOrder + 1;
                sortingOrderManager.setLargestSortingOrder(checkmarkSR.sortingOrder);
            }

            admit = 1;
        } else
        {
            if (!admitRedCheckmark.activeSelf)
            {
                admitRedCheckmark.SetActive(true);

                // make sure that the red checkmark is on top
                SpriteRenderer checkmarkSR = admitRedCheckmark.GetComponent<SpriteRenderer>();
                if (checkmarkSR != null)
                {
                    int topOrder = sortingOrderManager.getLargestSortingOrder();
                    checkmarkSR.sortingOrder = topOrder + 1;
                    sortingOrderManager.setLargestSortingOrder(checkmarkSR.sortingOrder);
                }

                admit = 1;
            } else
            {
                admitRedCheckmark.SetActive(false);

                // no decision is made since neither box is checked
                admit = 0;
            }
        }

        Debug.Log("ADMIT DECISION: " + admit);


        //// make red checkmark appear
        //redCheckmark.SetActive(true);

        // TODO: only play animation after review has been marked as complete
        //controller.Admit();
    }
}
