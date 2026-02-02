using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject paycheckSprite;
    public GameObject dialogue;
    public GameObject endDay;

    public TMP_Text paycheckText;

    // Called by animation event
    public void OnWalkFinished()
    {
        // Show the "Here is your paycheck" dialogue
        dialogue.SetActive(true);

        // show the paycheck
        paycheckSprite.SetActive(true);

        // show end day sprite
        endDay.SetActive(true);

        // Fill in the text from GameManager
        UpdatePaycheckText();
    }

    void UpdatePaycheckText()
    {
        Debug.Log("CALLED UPDATE PAYCHECK");

        int correctToday = GameManager.Instance.correctToday;
        int flaggedToday = GameManager.Instance.flaggedToday;
        int reportedToday = GameManager.Instance.reportedToday;
        int helpedToday = GameManager.Instance.helpedToday;


        // TODO: Delete all salary stuff
        //// calculate salary for the day
        //int salaryToday = (correctToday * 10);
        //int deductStudents = (5 - correctToday) - 1;
        //if (deductStudents > 0)
        //    salaryToday -= (5 * deductStudents);
        //salaryToday = Mathf.Max(0, salaryToday);

        //// accumulate total money
        //GameManager.Instance.AddSalary();

        //int totalMoney = GameManager.Instance.totalMoney;

        // deductions (if we want to add it later) $"Deductions: -${Mathf.Max(0, 5 * deductStudents)}\n" +
        // $"Correct Admissions: {correctToday}\n" +
        //$"Students Reported: {reportedToday}\n" +
        //$"Students Helped: {helpedToday}\n" +
        //$"Money Earned Today: ${salaryToday}\n" +
        //$"Total Bank: ${totalMoney}";

        paycheckText.text =
            $"Correct Admissions: {correctToday}\n" +
            $"Students Flagged: {flaggedToday}\n" +
            $"Students Reported: {reportedToday}\n" +
            $"Students Helped: {helpedToday}\n"; 
    }
}
