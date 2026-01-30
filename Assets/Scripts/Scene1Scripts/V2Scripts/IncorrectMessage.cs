using UnityEngine;

public class IncorrectMessage : MonoBehaviour
{
    // Called by OnClick for button to mimic closing the email tab
    public void HideIncorrectMessage()
    {
        gameObject.SetActive(false);
    }
}
