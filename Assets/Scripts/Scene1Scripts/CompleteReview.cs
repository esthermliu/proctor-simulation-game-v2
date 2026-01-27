using UnityEngine;

public class CompleteReview : MonoBehaviour
{
    public ReviewDecisionController controller;
    public GameObject admitRedCheckmark;
    public GameObject denyRedCheckmark;

    //public AdmitClick admitBox;
    //public DenyClick denyBox;


    void OnMouseDown()
    {
        if (admitRedCheckmark.activeSelf)
        {
            controller.Admit();
        } else if (denyRedCheckmark.activeSelf)
        {
            controller.Deny();
        }

        //Debug.Log("COMPLETE REVIEW CLICKED");
        //int admitBoxInfo = admitBox.getAdmit();
        //int denyBoxInfo = denyBox.getAdmit();

        //int finalDecision = Mathf.Max(admitBoxInfo, denyBoxInfo);

        //Debug.Log("admitBoxInfo " + admitBoxInfo + " and deny " + denyBoxInfo);
        //Debug.Log("final decision " + finalDecision);

        //// Can only complete review if a decision has been made
        //if (finalDecision > 0)
        //{
        //    // Admit = true
        //    if (finalDecision == 1)
        //    {
        //        controller.Admit();
        //    } else if (finalDecision == 2)
        //    {
        //        // Admit = false
        //        controller.Deny();
        //    }
        //}
    }

}
