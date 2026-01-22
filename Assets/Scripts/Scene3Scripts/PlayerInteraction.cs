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

        int reportedToday = GameManager.Instance.reportedToday;
        int correctToday = GameManager.Instance.correctToday;
        
        // calculate salary for the day
        int salaryToday = (correctToday * 10);
        int deductStudents = (5 - correctToday) - 1;
        if (deductStudents > 0)
            salaryToday -= (5 * deductStudents);
        salaryToday = Mathf.Max(0, salaryToday);

        // accumulate total money
        GameManager.Instance.AddSalary();

        int totalMoney = GameManager.Instance.totalMoney;

        paycheckText.text =
            $"Correct Admissions: {correctToday}\n" +
            $"Reported Students: {reportedToday}\n" +
            $"Money Earned Today: ${salaryToday}\n" +
            $"Total Bank: ${totalMoney}";
    }
}
