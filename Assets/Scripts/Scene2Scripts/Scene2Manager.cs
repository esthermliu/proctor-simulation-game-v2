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

    [Header("Event times")]
    public float badTime1 = 11f;
    public float questionTime = 45f;
    public float badTime2 = 80f;
    public float badTime3 = -1f;

    [Header("Bad Behavior Papers")]
    public GameObject badBehavior1Papers;
    public GameObject badBehavior2Papers;
    public GameObject badBehavior3Papers;
    public GameObject questionPapers;

    [Header("Student clickboxes")]
    public GameObject[] studentClickboxes;

    [Header("Audio Source (optional)")]
    public AudioSource audioSource;

    private bool bad1Triggered = false;
    private bool questionTriggered = false;
    private bool bad2Triggered = false;
    private bool bad3Triggered = false;

    private bool sceneEnded = false;

    // ==============================
    // TIMER SETTINGS
    // ==============================

    [Header("Timer Settings")]
    public TMP_Text timerText;

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

    private float colonToggleTimer = 0f;
    private bool showColon = true;

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

        colonToggleTimer += Time.deltaTime;
        if (colonToggleTimer >= 0.5f)
        {
            colonToggleTimer = 0f;
            showColon = !showColon;
        }

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

        string separator = showColon ? ":" : " ";
        timerText.text = $"0{minutes}{separator}{seconds:00}";
    }

    public void BeginExam() {
        // show help panel
        if (HelpManager.Instance != null)
        {
            HelpManager.Instance.ShowHelpPanel();
        }


        EventLogger.Log(new GameEvent
        {
            eventTypeEnum = EventType.exam_started,
        });
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

        // Hide any investigation panels + speech bubbles
        HidePanelIfActive<Investigate>(badBehavior1Papers);
        HidePanelIfActive<Investigate>(badBehavior2Papers);
        HidePanelIfActive<Investigate>(badBehavior3Papers);
        HidePanelIfActive<QuestionManager>(questionPapers);

        // allow the hover effect to work again
        foreach (GameObject clickbox in studentClickboxes)
        {
            if (clickbox != null)
            {
                clickbox.SetActive(true);
            }
        }

        // 4) Hide the help panel
        //HelpManager.Instance.HideHelpPanel();
    }

    private void HidePanelIfActive<T>(GameObject panel) where T : Component
    {
        if (panel == null) return;

        T component = panel.GetComponentInChildren<T>();

        if (component != null && component.gameObject.activeSelf && audioSource != null)
        {
            audioSource.Play();
        }

        panel.SetActive(false);
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

        // Disable all click boxes for all other students, making it
        // clear that during an ongoing event, you cannot click students!!
        foreach (GameObject clickbox in studentClickboxes)
        {
            if (clickbox != null)
            {
                clickbox.SetActive(false);
            }
        }
    }

    public void ResetOngoingEvent()
    {
        ongoingEvent = false;

        // Re-enable all click boxes for all other students, so hover effect works
        foreach (GameObject clickbox in studentClickboxes)
        {
            if (clickbox != null)
            {
                clickbox.SetActive(true);
            }
        }
    }

    public void EnableStartTime()
    {
        startTime = true;
        elapsedTime = 0f;
    }
}
