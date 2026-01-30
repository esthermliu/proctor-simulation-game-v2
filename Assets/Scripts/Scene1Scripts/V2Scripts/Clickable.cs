using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler
{
    public GameObject enlargedPaper;

    void Start()
    {
        // extra check in the code to hide enlarged paper
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1) Hide the small version of the paper
        gameObject.SetActive(false);


        // 2) Show the enlarged version of the paper IF it exists
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(true);

            // Bring enlarged paper to the FRONT of all other papers
            enlargedPaper.transform.SetAsLastSibling();
        } else
        {
            Debug.Log("ERROR (Clickable.cs): Missing enlarged version of paper");
        }
    }
}
