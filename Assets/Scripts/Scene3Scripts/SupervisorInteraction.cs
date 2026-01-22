using UnityEngine;

public class SupervisorInteraction : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject talkPromptSprite;     // “Click to talk”

    private bool clicked = false;

    void OnMouseDown()
    {
        if (clicked) return;
        clicked = true;

        // Hide talk prompt
        talkPromptSprite.SetActive(false);

        // Trigger walk animation
        playerAnimator.SetTrigger("WalkLeft");
    }
}
