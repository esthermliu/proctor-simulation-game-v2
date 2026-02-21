using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerDownHandler, 
IPointerEnterHandler, IPointerExitHandler
{
    public GameObject enlargedPaper;
    public Color hoverColor = new Color(0.7f, 0.7f, 0.7f, 1f);

    private Image image; 

    void Start()
    {
        // extra check in the code to hide enlarged paper
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(false);
        }

        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1) Hide the small version of the paper, reset color
        gameObject.SetActive(false);
        if (image != null)
        {
            image.color = Color.white; // reset to original color
        }


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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = Color.white; // reset to original color
        }
    }
}
