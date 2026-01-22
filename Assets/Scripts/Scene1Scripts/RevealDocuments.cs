using UnityEngine;

public class RevealDocuments : MonoBehaviour
{
    public GameObject studentID;
    public GameObject examTicket;

    public void ShowDocuments()
    {
        studentID.SetActive(true);
        examTicket.SetActive(true);
    }
}
