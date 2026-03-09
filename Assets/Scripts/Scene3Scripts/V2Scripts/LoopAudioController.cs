using UnityEngine;

public class LoopAudioController : MonoBehaviour
{
    public AudioSource audioSource;

    // Call from animation event to stop the loop
    public void StopLoop()
    {
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();  
        }
    }

    // Call from animation event to start looping again
    public void StartLoop()
    {
        if (audioSource != null)
        {
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }   
        }
    }
}