using UnityEngine;

public class ReviewDecisionController : MonoBehaviour
{
    [Header("Paper parents (small)")]
    public GameObject studentIDPaper;
    public GameObject examRegistrationPaper;
    public GameObject reviewFolder;
    public GameObject examGuide;
    public GameObject nextReviewFolderSmall;

    [Header("Paper parents (large)")]
    public GameObject studentIDEnlarged;
    public GameObject examRegistrationEnlarged;
    public GameObject reviewFolderEnlarged;
    public GameObject examGuideEnlarged;

    [Header("Student")]
    public StudentAnimationController student;

    [Header("Correct decision")]
    public bool isValid;

    [Header("Decision manager")]
    public DecisionManager decisionManager;

    private bool decisionMade = false;

    public void Admit()
    {
        if (decisionMade) return;
        decisionMade = true;

        // ---- GAME LOGIC ----
        
        if (isValid)
        {
            // adding the null check so we can run scene 1 on its own
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DecideStudentCorrectly();
            }
            
        } else
        {
            // else: incorrect admission (no increment, show message)
            decisionManager.ShowRedIndicator();
        }

        ResetPapers();

        student.Admit();

        // advance time
        TimeManager.Instance.AdvanceTime();
    }

    public void Deny()
    {
        if (decisionMade) return;
        decisionMade = true;

        // ---- GAME LOGIC ----
        if (!isValid)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DecideStudentCorrectly();
            }
        } else
        {
            // else: incorrect admission (no increment, show message)
            decisionManager.ShowRedIndicator();
        }

        ResetPapers();

        student.Deny();

        // advance time
        TimeManager.Instance.AdvanceTime();
    }

    private void ResetPapers()
    {
        studentIDPaper.SetActive(false);
        examRegistrationPaper.SetActive(false);
        studentIDEnlarged.SetActive(false);
        examRegistrationEnlarged.SetActive(false);
        reviewFolderEnlarged.SetActive(false);
        examGuideEnlarged.SetActive(false);

        // get the next misconduct review sheet (if there isn't another one, keep the latest one)
        reviewFolder.SetActive(false); 
        nextReviewFolderSmall.SetActive(true);

        //reviewFolder.SetActive(true);
        examGuide.SetActive(true);
    }
}
