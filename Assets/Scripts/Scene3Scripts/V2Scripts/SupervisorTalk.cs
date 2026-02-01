using UnityEngine;
using UnityEngine.EventSystems;

public class SupervisorTalk : MonoBehaviour, IPointerClickHandler
{
    public Animator playerAnimator;

    private bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // prevent double clicking
        if (clicked) return;
        clicked = true;

        // Hide talk prompt (script is attached to talk prompt)
        gameObject.SetActive(false);

        // Trigger walk animation
        playerAnimator.SetTrigger("WalkLeft");
    }
}
