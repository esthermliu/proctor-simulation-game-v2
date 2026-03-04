using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SupervisorTalk : MonoBehaviour, IPointerClickHandler
{
    [Header("Supervisor")]
    public Animator supervisorAnimator;

    [Header("Dialogue Manager")]
    public DialogueManager dialogueManager;

    [Header("Tutorial Box")]
    public GameObject tutorialBox;

    // Don't really need to check for double clicking (since sets game object to inactive), but can keep anyway
    private bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // prevent double clicking
        if (clicked) return;
        clicked = true;

        // Hide talk prompt (script is attached to talk prompt)
        gameObject.SetActive(false);

        // Hide the yellow tutorial box in case they didn't exit out (null check bc only day 1 tutorial box)
        if (tutorialBox != null)
        {
            tutorialBox.SetActive(false);
        }

        // Trigger the start of the dialogue
        dialogueManager.StartDialogue();

        // Since we started talking to the supervisor, trigger the start looking animation
        supervisorAnimator.SetTrigger("StartLooking");

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.supervisor_talk_clicked,
        });

    }
}
