using UnityEngine;

public class RevealPapers : MonoBehaviour
{
    [Header("Always exist")]
    public GameObject studentID;
    public GameObject examTicket;
    public GameObject materials;
    public GameObject reviewFolder;

    [Header("Optional")]
    public GameObject externalTicket;
    public GameObject accommodations;

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

        if (accommodations != null)
        {
            accommodations.SetActive(true);
        }

        if (reviewFolder != null)
        {
            reviewFolder.SetActive(true);
        }

    }
}
