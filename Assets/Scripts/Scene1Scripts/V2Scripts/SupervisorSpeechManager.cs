using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupervisorSpeechManager : MonoBehaviour
{
    [Header("All dialogue bubbles")]
    public GameObject[] dialogueBubbles;

    [Header("Timing")]
    [SerializeField] private float delayBetweenBubbles = 0.5f;

    [Header("Dialogue Pause Settings")]
    [SerializeField] private List<int> pauseIndices;

    [Header("Top Students Animation")]
    // index of speech bubble to start the top characters
    public int triggerTopStudentsIndex = 31;
    public GameObject gray1;
    public GameObject gray2;
    public GameObject gray3;
    public GameObject gray4;
    public GameObject gray5;

    [Header("Dialogue Pause Objects")]
    [SerializeField] private GameObject smallID;
    [SerializeField] private GameObject smallExamTicket;
    [SerializeField] private GameObject smallMaterials;
    [SerializeField] private GameObject smallReviewFolder;

    [Header("Tutorial Clickables")]
    [SerializeField] private TutorialClickable smallIDClickable;
    [SerializeField] private TutorialClickable smallExamTicketClickable;
    [SerializeField] private TutorialClickable smallExamGuideClickable;
    [SerializeField] private TutorialClickable smallMaterialsClickable;
    [SerializeField] private TutorialClickable smallFolderClickable;

    [Header("Buttons")]
    public GameObject clockInButton;
    public GameObject skipOrientationButton;

    private bool waitingForInteraction = false;
    private int currentIndex = 0;

    // whether this is the last interaction of the day
    [Header("Supervisor management")]
    public bool supervisorEndDay = false;

    [Header("Help Texts")]
    public string[] helpTexts;

    // This function should be triggered by an animation event after the supervisor character
    // stops moving
    public void StartDialogue()
    {
        // Need to show the "Skip Orientation" Button (only in A version)
        if (skipOrientationButton != null)
        {
            skipOrientationButton.SetActive(true);
        }

        // RELEASE 2 A/B Study (no longer needed)
        //if (skipOrientationButton != null && GameManager.Instance != null && GameManager.Instance.subversion == "A")
        //{
        //    // Need to show the "Skip Orientation" Button (only in A version)
        //    skipOrientationButton.SetActive(true);
        //}

        currentIndex = 0;
        ShowCurrentBubble();
    }

    public void OnSkipOrientation()
    {
        // the supervisor character should leave to the right, the gray characters should show up
        //       and the first student should show up on screen
        // OR, we can skip to the clock-in button scene

        // Let's FIRST activate all the top characters and make sure they are in position BEFORE
        // we call OnClickIn. We know all the gray characters are in position when the 6 seconds are up
        // for the animation

        // we can connect it to an animation event on gray character for student 1, which will call a function
        // that has the rest of the code below

        // stop the dialogue
        StopDialogue();

        // Activate all the gray characters
        ActivateTopCharacters();

        // specify a skip on the gray 1 character, so at the end of the animation, we will clock in
        gray1.GetComponent<GrayAnimationTrigger>().SetSkip(true);

        // Hide the skip orientation button
        skipOrientationButton.SetActive(false);

        // Hide all dialogue bubbles (should only need to disable the one at which the skip orientation button was clicked)
        //dialogueBubbles[currentIndex].SetActive(false);
        //// TO BE EXTRA SAFE, deactivate ALL bubbles
        //foreach (var bubble in dialogueBubbles)
        //{
        //    bubble.SetActive(false);
        //}
        //if (currentIndex + 1 < dialogueBubbles.Length) {
        //    dialogueBubbles[currentIndex + 1].SetActive(false);
        //}

        // Hide all materials
        SetActiveIfNotNull(smallID, false);
        SetActiveIfNotNull(smallExamTicket, false);
        SetActiveIfNotNull(smallMaterials, false);

        SetActiveIfNotNull(smallIDClickable?.enlargedPaper, false);
        SetActiveIfNotNull(smallExamTicketClickable?.enlargedPaper, false);
        SetActiveIfNotNull(smallMaterialsClickable?.enlargedPaper, false);
        SetActiveIfNotNull(smallExamGuideClickable?.enlargedPaper, false);
        SetActiveIfNotNull(smallFolderClickable?.enlargedPaper, false);

        // log the skip orientation event
        EventLogger.Log(new GameEvent
        {
            eventTypeEnum = EventType.orientation_skipped,
            index = currentIndex, // log at which dialogue bubble the orientation was skipped
        });

        GetComponent<StartDay>().BeginDay();
    }

    private void StopDialogue()
    {
        StopAllCoroutines();

        foreach (var bubble in dialogueBubbles)
        {
            var typewriter = bubble.GetComponent<TypewriterEffect>();
            if (typewriter != null)
            {
                typewriter.StopTyping();
            }
                
            bubble.SetActive(false);
        }
    }

    public void ResumeDialogue()
    {
        if (!waitingForInteraction) return;

        waitingForInteraction = false;

        if (HelpManager.Instance != null)
        {
            HelpManager.Instance.HideHelpPanel();
        }

        currentIndex++;
        ShowCurrentBubble();
    }

    private void SetActiveIfNotNull(GameObject obj, bool value)
    {
        if (obj != null)
        {
            obj.SetActive(value);
        }
    }


    private void ShowCurrentBubble()
    {
        if (currentIndex >= dialogueBubbles.Length)
        {
            if (skipOrientationButton != null)
            {
                // hide the skip orientation button
                skipOrientationButton.SetActive(false);
            }
            
            // end of tutorial speech, show the clock-in button
            clockInButton.SetActive(true);

            // if this is the last interaction, the supervisor just leaves after
            // the conversation
            if (supervisorEndDay)
            {
                gameObject.GetComponent<StartDay>().SupervisorExit();
            }

            return;
        }

        GameObject bubble = dialogueBubbles[currentIndex];
        bubble.SetActive(true);

        TypewriterEffect typewriter = bubble.GetComponent<TypewriterEffect>();

        if (typewriter != null)
        {
            typewriter.OnTypingComplete += HandleTypingComplete;
            typewriter.StartTyping();
        }
    }

    private void HandleTypingComplete()
    {
        StartCoroutine(AdvanceAfterDelay());
    }

    private IEnumerator AdvanceAfterDelay()
    {
        GameObject bubble = dialogueBubbles[currentIndex];
        TypewriterEffect typewriter = bubble.GetComponent<TypewriterEffect>();

        // Clean up event subscription
        typewriter.OnTypingComplete -= HandleTypingComplete;

        // Optional: leave bubble visible briefly
        yield return new WaitForSeconds(delayBetweenBubbles);

        // Hide current bubble
        bubble.SetActive(false);


        // STOP HERE if this is the pause index
        if (pauseIndices.Contains(currentIndex))
        {
            waitingForInteraction = true;
            if (pauseIndices[0] == currentIndex) {
                // DO NOT ALLOW PLAYER TO SKIP ORIENTATION ANYMORE
                if (skipOrientationButton != null)
                {
                    skipOrientationButton.SetActive(false);
                }

                // show the ID card at first pause
                smallID.SetActive(true);

                // jankiest thing ever lmao
                if (!smallIDClickable.IsDay1())
                {
                    // then, enable this ID card to be clickable
                    smallIDClickable.SetClickable(true);
                }

                if (HelpManager.Instance != null)
                {
                    HelpManager.Instance.ShowHelpPanel();
                }
            } else if (pauseIndices[1] == currentIndex)
            {
                // if we are at the second pause, then show the exam ticket and make it clickable
                smallExamTicket.SetActive(true);
                smallExamTicketClickable.SetClickable(true);
                smallExamTicketClickable.SetPause2(true);

                // jankiest thing ever lmao
                if (!smallIDClickable.IsDay1())
                {
                    // then, enable this ID card to be clickable
                    //smallIDClickable.SetClickable(true);
                }
                smallIDClickable.SetPause2(true);

                // enable the small exam guide to be clickable (if not null)
                if (smallExamGuideClickable != null)
                {
                    //smallExamGuideClickable.SetClickable(true);
                    smallExamGuideClickable.SetPause2(true);
                }

                if (HelpManager.Instance != null)
                {
                    HelpManager.Instance.ShowHelpPanel();
                }
            } else if (pauseIndices[2] == currentIndex)
            {
                // if we are at the third pause, then show the materials pouch and make it clickable
                smallMaterials.SetActive(true);
                smallMaterialsClickable.SetClickable(true);
                smallMaterialsClickable.SetPause3(true);

                // Don't make exam guide clickable yet
                //smallExamGuideClickable.SetClickable(true);
                smallExamGuideClickable.SetPause3(true);

                if (HelpManager.Instance != null)
                {
                    HelpManager.Instance.ShowHelpPanel();
                }
            } else if (pauseIndices[3] == currentIndex)
            {
                // if we are at the last pause, then make the review folder appear (not clickable yet)
                //smallFolderClickable.SetClickable(true);
                if (smallReviewFolder != null)
                {
                    smallReviewFolder.SetActive(true);
                }


                if (HelpManager.Instance != null)
                {
                    HelpManager.Instance.ShowHelpPanel();
                }
            }
            yield break; // IMPORTANT: stops dialogue flow
        } else if (triggerTopStudentsIndex == currentIndex)
        {
            // check if this index is when we need to trigger top students animation
            // if it is, then activate all the top characters
            ActivateTopCharacters();
        }

        // Advance
        currentIndex++;
        ShowCurrentBubble();
    }


    public void ActivateTopCharacters()
    {
        gray1.SetActive(true);
        gray2.SetActive(true);
        gray3.SetActive(true);
        gray4.SetActive(true);
        gray5.SetActive(true);
    }

}
