using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{
    public static Scene2Manager Instance;

    public List<Student> students;

    // Assign which student does what
    public int badStudent1Index = 0;
    public int questionStudentIndex = 1;
    public int badStudent2Index = 4;

    // Janky solution for now, but hardcoded day 3 additional bad student
    [Header("Day 3 Specific Additional Bad Behavior")]
    public bool day3 = false;
    public int badStudent3Index = -1;

    [Header("Pencils Down Bubble")]
    public GameObject pencilsDownBubble;

    public float badTime1 = 11f;
    public float questionTime = 45f;
    public float badTime2 = 80f;
    public float badTime3 = -1f;

    private bool bad1Triggered = false;
    private bool questionTriggered = false;
    private bool bad2Triggered = false;
    private bool bad3Triggered = false;

    private bool sceneEnded = false;

    public TMP_Text timerText;

    // ==============================
    // TIMER SETTINGS
    // ==============================

    [Header("Timer Settings")]

    // How long the scene lasts (seconds)
    public float totalDuration = 110f;

    // What time the clock should START at (minutes + seconds)
    public int startMinutes = 1;
    public int startSeconds = 0;

    private float elapsedTime = 0f;
    public float ElapsedTime { get => elapsedTime; }
    private float startTimeInSeconds;

    private bool ongoingEvent = false;
    private bool startTime = false;

    [Header("Help Text")]
    public string helpText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Convert starting display time to seconds
        startTimeInSeconds = (startMinutes * 60f) + startSeconds;
    }

    void Update()
    {
        if (!startTime) return;

        // Internal timer counts up from 0
        elapsedTime += Time.deltaTime;
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, totalDuration);

        UpdateTimerUI();

        if (!bad1Triggered && elapsedTime >= badTime1)
        {
            students[badStudent1Index].ShowIndicator();
            bad1Triggered = true;
        }

        if (!questionTriggered && elapsedTime >= questionTime)
        {
            students[questionStudentIndex].ShowIndicator();
            questionTriggered = true;
        }

        if (!bad2Triggered && elapsedTime >= badTime2)
        {
            students[badStudent2Index].ShowIndicator();
            bad2Triggered = true;
        }

        // SPECIFIC additional bad behavior for Day 2
        if (day3)
        {
            if (!bad3Triggered && elapsedTime >= badTime3)
            {
                students[badStudent3Index].ShowIndicator();
                bad3Triggered = true;
            }
        }

        if (elapsedTime >= totalDuration)
        {
            EndScene();
        }
    }

    void UpdateTimerUI()
    {
        // Add offset to elapsed time
        float displayedTime = startTimeInSeconds + elapsedTime;

        int totalSeconds = Mathf.FloorToInt(displayedTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"0{minutes}:{seconds:00}";
    }

    public void BeginExam() {
        HelpManager.Instance.SetHelpText(helpText);
        HelpManager.Instance.ShowHelpPanel();
    }

    void EndScene()
    {
        if (sceneEnded) return;
        sceneEnded = true;

        // 1) Show the Pencils Down bubble
        pencilsDownBubble.SetActive(true);


        // 2) Make extra check that any questions were answered, show notification otherwise
        students[questionStudentIndex].CheckQuestionAnswered();

        // 3) Make sure to hide the question guide and any speech bubbles if the question
        // is still ongoing
        students[questionStudentIndex].HideQuestion();
        students[questionStudentIndex].HideGuide();
        students[questionStudentIndex].HideGoodResponse();
        students[questionStudentIndex].HideBadResponse();

        // 4) Hide the help panel
        HelpManager.Instance.HideHelpPanel();
    }

    // ================================================
    // Ongoing event control
    // ================================================

    public bool ExistsOngoingEvent()
    {
        return ongoingEvent;
    }

    public void SetOngoingEvent()
    {
        ongoingEvent = true;
    }

    public void ResetOngoingEvent()
    {
        ongoingEvent = false;
    }

    public void EnableStartTime()
    {
        startTime = true;
        elapsedTime = 0f;
    }
}
