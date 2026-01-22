using UnityEngine;
public class ExamGuideManager : MonoBehaviour
{
    public GameObject cseListings;
    public GameObject mathListings;
    public GameObject philListings;
    public GameObject chemListings;

    private static int topSortingOrder = 100;  // Start high to avoid conflicts with draggables

    void Start()
    {
        HideAll();
    }

    public void ShowCSE()
    {
        HideAll();
        cseListings.SetActive(true);
        BringToFront(cseListings);
    }

    public void ShowMath()
    {
        HideAll();
        mathListings.SetActive(true);
        BringToFront(mathListings);
    }

    public void ShowPhil()
    {
        HideAll();
        philListings.SetActive(true);
        BringToFront(philListings);
    }

    public void ShowChem()
    {
        HideAll();
        chemListings.SetActive(true);
        BringToFront(chemListings);
    }

    void HideAll()
    {
        cseListings.SetActive(false);
        mathListings.SetActive(false);
        philListings.SetActive(false);
        chemListings.SetActive(false);
    }

    void BringToFront(GameObject listings)
    {
        topSortingOrder++;

        // Set sorting order for all SpriteRenderers in the listings
        SpriteRenderer[] sprites = listings.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer sr in sprites)
        {
            sr.sortingOrder = topSortingOrder;
        }

        // Set sorting order for all TextMeshPro components
        TMPro.TextMeshPro[] texts = listings.GetComponentsInChildren<TMPro.TextMeshPro>(true);
        foreach (TMPro.TextMeshPro txt in texts)
        {
            txt.sortingOrder = topSortingOrder + 1;  // Text slightly above sprites
        }

        // If using TextMeshProUGUI instead (Canvas-based)
        TMPro.TextMeshProUGUI[] uiTexts = listings.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
        foreach (TMPro.TextMeshProUGUI txt in uiTexts)
        {
            Canvas canvas = txt.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingOrder = topSortingOrder + 1;
            }
        }
    }
}