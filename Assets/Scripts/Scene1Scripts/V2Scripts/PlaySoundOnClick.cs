using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnClick : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource; // assign in Inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip); // plays the assigned AudioClip
        }
    }
}