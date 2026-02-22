using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameState state = new GameState();

    // TODO: remove money
    public int totalMoney = 0;

    // Analytics data
    public string sessionId = null;
    public string subversion = "A"; // TODO: set this based on A/B testing

    private void Awake()
    {
        sessionId = System.Guid.NewGuid().ToString();
        // Log the game start event with the session ID
        EventLogger.Log(new GameEvent
        {
            eventTypeEnum = EventType.session_start,
        });

        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // logging for scene changes
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Log the scene entered event with the session ID and scene name
        EventLogger.Log(
            new GameEvent
            {
                eventTypeEnum = EventType.scene_entered,
                sceneName = scene.name,
                gameState = state,
            }
        );

    }

    // ----- Called when a student is correctly admitted/denied -----
    public void DecideStudentCorrectly()
    {
        state.correctToday++;
        state.totalCorrect++;
    }

    // ----- Called when a student is reported -----
    public void ReportStudent()
    {
        state.reportedToday++;
        state.totalReported++;
    }

    // ----- Called when a student is helped (question answered correctly) -----
    public void HelpStudent()
    {
        state.helpedToday++;
        state.totalHelped++;
    }

    // ----- Called when a student is flagged -----
    public void FlagStudent()
    {
        state.flaggedToday++;
        state.totalFlagged++;
    }

    // ---- called at EOD to add up salary ---
    public void AddSalary()
    {
        totalMoney += (state.correctToday * 10);
        int deductStudents = (5 - state.correctToday) - 1;
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
        state.ResetDailyStats();
    }
}
