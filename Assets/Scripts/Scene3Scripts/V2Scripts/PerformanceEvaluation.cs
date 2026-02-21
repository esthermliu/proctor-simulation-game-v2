using UnityEngine;
using TMPro;

public class PerformanceEvaluation : MonoBehaviour
{
    [Header("Performance Evaluation Info")]
    public int dayNumber = 1;
    public TMP_Text correctAdmissionsText;
    public TMP_Text studentsFlaggedText;
    public TMP_Text studentsReportedText;
    public TMP_Text studentsHelpedText;
    public TMP_Text overallAssessmentText;


    public void ShowPerformanceEvaluation()
    {
        // make the evaluation form show up (make sure to attach this script to the eval)
        gameObject.SetActive(true);

        // show statistics on form
        UpdatePerformanceEvalText();
    }


    void UpdatePerformanceEvalText()
    {
        // Do not update Paycheck text if no game manager is present
        if (GameManager.Instance == null)
        {
            return;
        }

        int correctToday = GameManager.Instance.state.correctToday;
        int flaggedToday = GameManager.Instance.state.flaggedToday;
        int reportedToday = GameManager.Instance.state.reportedToday;
        int helpedToday = GameManager.Instance.state.helpedToday;


        // update text
        correctAdmissionsText.text = correctToday + "";
        studentsFlaggedText.text = flaggedToday + "";
        studentsReportedText.text = reportedToday + "";
        studentsHelpedText.text = helpedToday + "";

        overallAssessmentText.text = OverallAssessment();
    }

    private string OverallAssessment()
    {
        if (dayNumber == 1)
        {
            return OverallAssessmentDay1();
        }

        return "";
    }

    private string OverallAssessmentDay1()
    {
        int correctToday = GameManager.Instance.state.correctToday;
        int flaggedToday = GameManager.Instance.state.flaggedToday;
        int reportedToday = GameManager.Instance.state.reportedToday;
        int helpedToday = GameManager.Instance.state.helpedToday;

        bool satisfactory = correctToday >= 4 && flaggedToday >= 1 && reportedToday == 1 && helpedToday == 1;
        bool requiresReview = correctToday >= 3 && flaggedToday >= 1 && reportedToday < 2 && helpedToday == 1;
        if (satisfactory)
        {
            return "SATISFACTORY";
        }
        else if (requiresReview)
        {
            return "REQUIRES REVIEW";
        }
        else
        {
            return "INADEQUATE";
        }
    }
}
