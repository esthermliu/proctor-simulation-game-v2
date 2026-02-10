using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialClickable : MonoBehaviour, IPointerDownHandler
{
    public GameObject enlargedPaper;

    public SupervisorPause2Manager supervisorPause2Manager;
    public SupervisorPause3Manager supervisorPause3Manager;

    public string whatAmI; // ID, ticket, or guide

    // keeps track of when this item can be clicked, set to false for everything at first
    private bool clickable = false;

    // keeps track of whether we are in pause 2
    private bool pause2 = false;

    // keeps track of whether we are in pause 3
    private bool pause3 = false;

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
        // Check if the item can be clicked
        if (!clickable) return;


        // check if we are in pause 2 and have to keep track of whether we were clicked or not
        if (pause2)
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
