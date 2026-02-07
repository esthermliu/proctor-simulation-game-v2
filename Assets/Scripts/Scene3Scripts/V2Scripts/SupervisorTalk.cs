using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SupervisorTalk : MonoBehaviour, IPointerClickHandler
{
    [Header("Supervisor")]
    public Animator supervisorAnimator;

    [Header("Dialogue Manager")]
    public DialogueManager dialogueManager;

    // Don't really need to check for double clicking (since sets game object to inactive), but can keep anyway
    private bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // prevent double clicking
        if (clicked) return;
        clicked = true;

        // Hide talk prompt (script is attached to talk prompt)
        gameObject.SetActive(false);

        // Trigger the start of the dialogue
        dialogueManager.StartDialogue();

        // Since we started talking to the supervisor, trigger the start looking animation
        supervisorAnimator.SetTrigger("StartLooking");

    }
}
