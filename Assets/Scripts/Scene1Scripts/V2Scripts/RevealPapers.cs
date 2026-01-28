using UnityEngine;

public class RevealPapers : MonoBehaviour
{
    public GameObject studentID;
    public GameObject examTicket;
    public GameObject materials;

    public void ShowDocuments()
    {
        studentID.SetActive(true);
        examTicket.SetActive(true);
        materials.SetActive(true);
    }
}
