using UnityEngine;

public class PlaySpeechBubbleAudio : MonoBehaviour
{
    public AudioSource audioSource;

    void OnEnable()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}