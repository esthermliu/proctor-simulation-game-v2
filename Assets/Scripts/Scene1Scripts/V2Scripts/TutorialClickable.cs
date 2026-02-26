using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialClickable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject enlargedPaper;
    public float hoverScale = 1.1f;

    public SupervisorPause2Manager supervisorPause2Manager;
    public SupervisorPause3Manager supervisorPause3Manager;

    public string whatAmI; // ID, ticket, or guide

    // keep track of which tutorial day we are on
    public bool day2 = false;

    // keeps track of when this item can be clicked, set to false for everything at first
    private bool clickable = false;

    // keeps track of whether we are in pause 2
    private bool pause2 = false;

    // keeps track of whether we are in pause 3
    private bool pause3 = false;

    private Image image;
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

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
        // Check if the item can be clicked
        if (!clickable) return;

        // reset to original scale
        transform.localScale = originalScale;

        // check if we are in pause 2 and have to keep track of whether we were clicked or not
        if (pause2 && !day2)
        {
            if (whatAmI.Equals("ID"))
            {
                supervisorPause2Manager.IDCardClicked();
            } else if (whatAmI.Equals("ticket"))
            {
                supervisorPause2Manager.ExamTicketClicked();
            } else if (whatAmI.Equals("guide"))
            {
                supervisorPause2Manager.ExamGuideClicked();
            }
        } else if (pause3)
        {
            if (whatAmI.Equals("materials"))
            {
                supervisorPause3Manager.MaterialsClicked();
            } else if (whatAmI.Equals("guide"))
            {
                supervisorPause3Manager.ExamGuideClicked();
            }
        }


        // 1) Hide the small version of the paper
        gameObject.SetActive(false);


        // 2) Show the enlarged version of the paper IF it exists
        if (enlargedPaper != null)
        {
            enlargedPaper.SetActive(true);

            // Bring enlarged paper to the FRONT of all other papers
            enlargedPaper.transform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("ERROR (Clickable.cs): Missing enlarged version of paper");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!clickable) return;
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!clickable) return;
        transform.localScale = originalScale;
    }

    public void SetClickable(bool clickable)
    {
        this.clickable = clickable;
    }

    public void SetPause2(bool occurring)
    {
        pause2 = occurring;
    }

    public void SetPause3(bool occurring)
    {
        pause3 = occurring;
    }
}
