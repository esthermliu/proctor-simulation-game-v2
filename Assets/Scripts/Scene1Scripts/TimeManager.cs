using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public int startHour;
    public int startMinutes;

    private int totalMinutes = 0; // total minutes elapsed

    private int studentDuration = 2; // 2 minutes per student

    // Call this after a player makes a decision
    public void AdvanceTime()
    {
        // TODO: call the other advancetime function
        totalMinutes += studentDuration;
        UpdateTimeDisplay();
    }

    public void AdvanceTime(int time)
    {
        totalMinutes += time;
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay()
    {
        int hours = (startHour + (startMinutes + totalMinutes) / 60) % 24; // assume day starts at 8:00 AM
        int minutes = (startMinutes + totalMinutes) % 60;

        string ampm = hours >= 12 ? "PM" : "AM";
        int displayHour = hours % 12;
        if (displayHour == 0) displayHour = 12;

        timeText.text = $"{hours}:{minutes:00} {ampm}";
    }
}
