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
        // Do not update Paycheck text if no game manager is present
        if (GameManager.Instance == null)
        {
            return;
        }

        int correctToday = GameManager.Instance.state.correctToday;
        int flaggedToday = GameManager.Instance.state.flaggedToday;
        int reportedToday = GameManager.Instance.state.reportedToday;
        int helpedToday = GameManager.Instance.state.helpedToday;

        paycheckText.text =
            $"Correct Admissions: {correctToday}\n" +
            $"Students Flagged: {flaggedToday}\n" +
            $"Students Reported: {reportedToday}\n" +
            $"Students Helped: {helpedToday}\n"; 
    }
}
