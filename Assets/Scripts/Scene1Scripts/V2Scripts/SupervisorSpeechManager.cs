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

    [Header("Tutorial Clickables")]
    [SerializeField] private TutorialClickable smallIDClickable;
    [SerializeField] private TutorialClickable smallExamTicketClickable;
    [SerializeField] private TutorialClickable smallExamGuideClickable;
    [SerializeField] private TutorialClickable smallMaterialsClickable;
    [SerializeField] private TutorialClickable smallFolderClickable;

    [Header("Clock In Btn")]
    public GameObject clockInButton;

    private bool waitingForInteraction = false;
    private int currentIndex = 0;

    // This function should be triggered by an animation event after the supervisor character
    // stops moving
    public void StartDialogue()
    {
        currentIndex = 0;
        ShowCurrentBubble();
    }


    public void ResumeDialogue()
    {
        if (!waitingForInteraction) return;

        waitingForInteraction = false;

        currentIndex++;
        ShowCurrentBubble();
    }


    private void ShowCurrentBubble()
    {
        if (currentIndex >= dialogueBubbles.Length)
        {
            // end of tutorial speech, show the clock-in button
            clockInButton.SetActive(true);
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
                // show the ID card at first pause
                smallID.SetActive(true);

                // then, enable this ID card to be clickable
                smallIDClickable.SetClickable(true);
            } else if (pauseIndices[1] == currentIndex)
            {
                // if we are at the second pause, then show the exam ticket and make it clickable
                smallExamTicket.SetActive(true);
                smallExamTicketClickable.SetClickable(true);
                smallExamTicketClickable.SetPause2(true);

                // enable the small ID card to be clickable
                smallIDClickable.SetClickable(true);
                smallIDClickable.SetPause2(true);

                // enable the small exam guide to be clickable
                smallExamGuideClickable.SetClickable(true);
                smallExamGuideClickable.SetPause2(true);
            } else if (pauseIndices[2] == currentIndex)
            {
                // if we are at the third pause, then show the materials pouch and make it clickable
                smallMaterials.SetActive(true);
                smallMaterialsClickable.SetClickable(true);
                smallMaterialsClickable.SetPause3(true);

                // also make the exam guide clickable
                smallExamGuideClickable.SetClickable(true);
                smallExamGuideClickable.SetPause3(true);
            } else if (pauseIndices[3] == currentIndex)
            {
                // if we are at the last pause, then make the review folder clickable
                smallFolderClickable.SetClickable(true);
            }
            yield break; // IMPORTANT: stops dialogue flow
        } else if (triggerTopStudentsIndex == currentIndex)
        {
            // check if this index is when we need to trigger top students animation
            // if it is, then activate all the top characters
            gray1.SetActive(true);
            gray2.SetActive(true);
            gray3.SetActive(true);
            gray4.SetActive(true);
            gray5.SetActive(true);
        }

        // Advance
        currentIndex++;
        ShowCurrentBubble();
    }

}
