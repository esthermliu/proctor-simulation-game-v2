using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public GameObject redIndicator; // drag the red sprite here

    // Call this when player makes a wrong decision
    public void ShowRedIndicator()
    {
        if (redIndicator != null)
        {
            redIndicator.SetActive(true);
        }
    }
}
