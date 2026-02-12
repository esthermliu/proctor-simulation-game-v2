using UnityEngine;

public class CloseEmail : MonoBehaviour
{
    public GameObject buttonToShow;

    // Called by OnClick for button to mimic closing the email tab
    public void HideEmail()
    {
        gameObject.SetActive(false);

        // show begin exam button
        buttonToShow.SetActive(true);
    }
}
