using UnityEngine;

public class CompleteReview : MonoBehaviour
{
    public ReviewDecisionController controller;
    public GameObject admitRedCheckmark;
    public GameObject denyRedCheckmark;

    void OnMouseDown()
    {
        if (admitRedCheckmark.activeSelf)
        {
            controller.Admit();
        } else if (denyRedCheckmark.activeSelf)
        {
            controller.Deny();
        }
    }

}
