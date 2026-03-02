using UnityEngine;

public class GrayAnimationTrigger : MonoBehaviour
{
    [Header("Link next gray animator in line")]
    public Animator nextGrayAnimator;

    [Header("Link to StartDay script on Supervisor")]
    public StartDay supervisorStartDay;

    public bool skip = false;
    
    public void NextGrayAnimation(string triggerName)
    {
        if (nextGrayAnimator != null)
        {
            nextGrayAnimator.SetTrigger(triggerName);
        } 
    }

    // make sure that we only start the day/clock in after the gray characters are in position
    // otherwise, animation looks very janky
    public void OnSkipOrientationAnimationEvent()
    {
        // only skip if the button was clicked
        if (skip)
        {
            // Get the StartDay component on the supervisor and call the OnClockIn function
            supervisorStartDay.OnClockIn();

            // make sure we don't go forward by 20 minutes (this is very jank)
            // this is bc onClockIn also calls BeginDay
            TimeManager.Instance.AdvanceTime(-10);
        }
    }

    public void SetSkip(bool skip)
    {
        this.skip = skip;
    }
}
