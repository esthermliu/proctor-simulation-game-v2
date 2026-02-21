using UnityEngine;

public class RevealPapers : MonoBehaviour
{
    public GameObject studentID;
    public GameObject examTicket;
    public GameObject materials;
    public GameObject externalTicket;

    public void ShowDocuments()
    {
        if (studentID != null)
        {
            studentID.SetActive(true);
        }

        if (examTicket != null)
        {
            examTicket.SetActive(true);
        }

        if (materials != null)
        {
            materials.SetActive(true);
        }

        if (externalTicket != null)
        {
            externalTicket.SetActive(true);
        }
    }
}
