using UnityEngine;

public class GrayAnimationTrigger : MonoBehaviour
{
    [Header("Link next gray animator in line")]
    public Animator nextGrayAnimator;

    public void NextGrayAnimation(string triggerName)
    {
        nextGrayAnimator.SetTrigger(triggerName);
    }
}
