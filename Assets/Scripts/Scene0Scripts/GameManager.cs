using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ----- Per Day Stats -----
    public int correctToday = 0;
    public int reportedToday = 0;

    // ----- Total Stats -----
    public int totalCorrect = 0;
    public int totalReported = 0;
    public int totalMoney = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ----- Called when a student is correctly admitted/denied -----
    public void DecideStudentCorrectly()
    {
        correctToday++;
        totalCorrect++;
    }

    // ----- Called when a student is reported -----
    public void ReportStudent()
    {
        reportedToday++;
        totalReported++;
    }

    // ---- called at EOD to add up salary ---
    public void AddSalary()
    {
        totalMoney += (correctToday * 10);
        int deductStudents = (5 - correctToday) - 1;
        if (deductStudents > 0)
        {
            totalMoney -= (5 * deductStudents);
        }

        // Clamp so money never goes below 0
        totalMoney = Mathf.Max(0, totalMoney);
    }

    // ----- Reset per-day stats (call at start of new day) -----
    public void ResetDailyStats()
    {
        correctToday = 0;
        reportedToday = 0;
    }
}
