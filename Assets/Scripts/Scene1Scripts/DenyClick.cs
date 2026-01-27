using UnityEngine;

public class DenyClick : MonoBehaviour
{
    public ReviewDecisionController controller;
    public GameObject denyRedCheckmark;
    public GameObject admitRedCheckmark;

    public SortingOrderManager sortingOrderManager;

    // 0 = no decision, 1 is YES admit, 2 is NO admit
    private int admit;

    void Start()
    {
        admit = 0;
    }

    public int getAdmit()
    {
        return admit;
    }

    void OnMouseDown()
    {
        // When the player clicks the deny checkbox
        // Case 1: the admit checkbox is already checked
        //   --> 1a) The deny checkbox must NOT already be checked
        //            --> we check the deny checkbox AND uncheck the admit checkbox
        // Case 2: the admit checkbox is NOT already checked
        //   --> 1a) The deny checkbox is NOT already checked
        //          --> we check the deny checkbox
        //   --> 1b) The deny checkbox IS already checked
        //          --> we uncheck the deny checkbox
        Debug.Log("DENY ZONE CLICKED");


        if (admitRedCheckmark.activeSelf)
        {
            admitRedCheckmark.SetActive(false);
            denyRedCheckmark.SetActive(true);

            // TODO: refactor code
            // make sure that the red checkmark is on top
            SpriteRenderer checkmarkSR = denyRedCheckmark.GetComponent<SpriteRenderer>();
            if (checkmarkSR != null)
            {
                int topOrder = sortingOrderManager.getLargestSortingOrder();
                checkmarkSR.sortingOrder = topOrder + 1;
                sortingOrderManager.setLargestSortingOrder(checkmarkSR.sortingOrder);
            }

            admit = 2;
        }
        else
        {
            if (!denyRedCheckmark.activeSelf)
            {
                denyRedCheckmark.SetActive(true);

                SpriteRenderer checkmarkSR = denyRedCheckmark.GetComponent<SpriteRenderer>();
                if (checkmarkSR != null)
                {
                    int topOrder = sortingOrderManager.getLargestSortingOrder();
                    checkmarkSR.sortingOrder = topOrder + 1;
                    sortingOrderManager.setLargestSortingOrder(checkmarkSR.sortingOrder);
                }


                admit = 2;
            }
            else
            {
                denyRedCheckmark.SetActive(false);

                // no decision is made since neither box is checked
                admit = 0;
            }
        }

        Debug.Log("ADMIT DECISION: " + admit);

        //controller.Deny();
    }
}
