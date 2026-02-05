using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene2Manager : MonoBehaviour
{
    public List<Student> students;

    // Assign which student does what
    public int badStudent1Index = 0;
    public int questionStudentIndex = 1;
    public int badStudent2Index = 4;

    private float badTime1;
    private float questionTime;
    private float badTime2;

    private bool bad1Triggered = false;
    private bool questionTriggered = false;
    private bool bad2Triggered = false;

    private bool sceneEnded = false;

    public TMP_Text timerText;
    private float timeRemaining = 60f;

    // Keep track of whether a review or question is ongoing (there can only be one ongoing
    // review/question at a time); true if exists ongoing event, false otherwise
    // Starts as false initially
    private bool ongoingEvent = false;

    void Start()
    {
        badTime1 = Random.Range(5f, 15f);
        questionTime = Random.Range(20f, 40f);
        badTime2 = Random.Range(30f, 50f);

        Debug.Log($"Bad1 at {badTime1}, Question at {questionTime}, Bad2 at {badTime2}");
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Clamp(timeRemaining, 0f, 60f);

        UpdateTimerUI();

        // Show Red Indicator 1
        if (!bad1Triggered && timeRemaining <= 60f - badTime1)
        {
            students[badStudent1Index].ShowIndicator();
            bad1Triggered = true;
        }

        if (!questionTriggered && timeRemaining <= 60f - questionTime)
        {
            students[questionStudentIndex].ShowIndicator();
            questionTriggered = true;
        }

        if (!bad2Triggered && timeRemaining <= 60f - badTime2)
        {
            students[badStudent2Index].ShowIndicator();
            bad2Triggered = true;
        }

        if (timeRemaining <= 0f)
        {
            EndScene();
        }
    }

    void UpdateTimerUI()
    {
        int totalSeconds = Mathf.CeilToInt(timeRemaining);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"{minutes}:{seconds:00}";
    }


    void EndScene()
    {
        if (sceneEnded) return;
        sceneEnded = true;

        Debug.Log("Scene 2 finished");

        SceneManager.LoadScene("Scene3_Day1_V2");
    }

    // ================================================
    // Functions to make sure only one ongoing event
    // ================================================

    // Function to check whether the player can click on a new student (aka start a new
    // investigation or question)
    // Returns true if there is an ongoing event (investigation/question), false otherwise
    public bool ExistsOngoingEvent()
    {
        return ongoingEvent;
    }

    // Function called when a new event is started
    public void SetOngoingEvent()
    {
        ongoingEvent = true;
    }

    // Function called when ongoing event ends
    public void ResetOngoingEvent()
    {
        ongoingEvent = false;
    }
}
