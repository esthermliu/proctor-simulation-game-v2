using UnityEngine;

public class HideCharacter : MonoBehaviour
{
    // This function should be called by the animation event to hide the
    // green or red character after the walk off animation completes
    public void HideCharacterAfterAnimation()
    {
        gameObject.SetActive(false);
    }
}
