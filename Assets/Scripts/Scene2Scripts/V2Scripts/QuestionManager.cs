using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour
{
    [Header("Link to Scene 2 Manager")]
    public Scene2Manager scene2Manager;

    [Header("Link to corresponding student")]
    public Student student;

    [Header("Typewriter Reference")]
    public TypewriterEffect typewriterQuestion;
    public TypewriterEffect typewriterCorrect;
    public TypewriterEffect typewriterIncorrect;

    [Header("Answer Buttons")]
    public GameObject[] optionButtons;

    [Header("Guide image")]
    public GameObject guideImage;

    private Animator studentAnimator;

    // keep track of options that we already selected
    private HashSet<GameObject> permanentlyDisabledButtons = new HashSet<GameObject>();

    void Start()
    {
        // Get the Animator component from the student
        if (student != null)
        {
            studentAnimator = student.GetComponent<Animator>();
        }

        // Subscribe to question typewriter finish event
        if (typewriterQuestion != null)
        {
            typewriterQuestion.OnTypingComplete += HandleQuestionTypingFinished;
        }     
    }

    public void OnCorrectOptionClick()
    {
        // Disable buttons immediately
        SetButtonsActive(false);

        // Correct: show good response, hide the exam guide, hide the bad response if visible
        student.ShowGoodResponse();

        // Subscribe BEFORE typing starts
        // Look at HandleTypingFinished to deal with ending the event + other stuff
        typewriterCorrect.OnTypingComplete += HandleTypingFinished;


        // If Game Manager exists, then increment students helped that day
        if (GameManager.Instance != null)
        {
            GameManager.Instance.HelpStudent();
        }

        // make sure to note that the question WAS answered, so no notification shows up
        student.QuestionAnswered();

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.question_answered,
            isCorrect = true,
            studentName = student.name,
            elapsedTime = scene2Manager.ElapsedTime,
        });
    }

    public void OnIncorrectOptionClick(GameObject clickedButton)
    {
        // Add to permanent disabled set
        permanentlyDisabledButtons.Add(clickedButton);

        // Disable buttons immediately
        SetButtonsActive(false);

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.question_answered,
            isCorrect = false,
            elapsedTime = scene2Manager.ElapsedTime,
            studentName = student.name,
        });
    }

    // On incorrect, in button UI, we can activate the corresponding bad response
    // then, need to call a function to set typewriterIncorrect to the correct
    // typewriter element for the corresponding bad response, and start the HandleIncorrectTypingFinished
    public void SetTypewriterIncorrect(TypewriterEffect incorrectTypewriter)
    {
        this.typewriterIncorrect = incorrectTypewriter;

        // keep track of when the bad response is done typing
        typewriterIncorrect.OnTypingComplete += HandleIncorrectTypingFinished;
    }


    private void HandleTypingFinished()
    {
        StartCoroutine(HideGuideWithDelay());
    }

    private IEnumerator HideGuideWithDelay()
    {
        yield return new WaitForSeconds(1f);

        // hide the good response + guide
        student.HideGoodResponse();
        student.HideGuide();

        // Transition from STILL to IDLE (writing) animation (only trigger animation after the
        // guide has been hidden)
        if (studentAnimator != null)
        {
            studentAnimator.SetTrigger("StartIdle");
        }

        // End the question event
        scene2Manager.ResetOngoingEvent();

        // Unsubscribe so it doesn't fire again later
        typewriterCorrect.OnTypingComplete -= HandleTypingFinished;
    }

    // Extra check to make sure all events are unsubscribed from
    private void OnDisable()
    {
        if (typewriterCorrect != null)
        {
            typewriterCorrect.OnTypingComplete -= HandleTypingFinished;
        }

        if (typewriterIncorrect != null)
        {
            typewriterIncorrect.OnTypingComplete -= HandleIncorrectTypingFinished;
        }
            
    }

    private void SetButtonsActive(bool active)
    {
        foreach (GameObject button in optionButtons)
        {
            if (active)
            {
                // Only re-enable if NOT permanently disabled
                if (!permanentlyDisabledButtons.Contains(button))
                    button.SetActive(true);
            }
            else
            {
                button.SetActive(false);
            }
        }
    }

    private void HandleIncorrectTypingFinished()
    {
        StartCoroutine(ReenableButtonsAfterDelay());
    }

    private IEnumerator ReenableButtonsAfterDelay()
    {
        // slight delay before you can try again
        yield return new WaitForSeconds(1f);

        // hide the bad response
        typewriterIncorrect.gameObject.SetActive(false);

        // Re-enable buttons after typing finishes
        SetButtonsActive(true);

        // Unsubscribe
        typewriterIncorrect.OnTypingComplete -= HandleIncorrectTypingFinished;
    }

    private void HandleQuestionTypingFinished()
    {
        StartCoroutine(ShowButtonsAfterDelay());
    }

    private IEnumerator ShowButtonsAfterDelay()
    {
        // 1-second delay after typing finishes
        yield return new WaitForSeconds(1f);

        // hide the question, makes it harder since you have to remember the question HA! >:)
        student.HideQuestion();

        // show the guide image now (which has all buttons there too)
        guideImage.SetActive(true);

        // Unsubscribe so it only fires once
        if (typewriterQuestion != null)
        {
            typewriterQuestion.OnTypingComplete -= HandleQuestionTypingFinished;
        } 
    }
}
