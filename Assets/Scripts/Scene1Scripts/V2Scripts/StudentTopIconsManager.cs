using UnityEngine;

public class StudentTopIconsManager : MonoBehaviour
{
    [Header("Link to corresponding colored characters")]
    public GameObject grayCharacter;
    public GameObject greenCharacter;
    public GameObject redCharacter;

    public void ShowGreenCharacter()
    {
        // 1) deactivate the gray character
        grayCharacter.SetActive(false); 

        // 2) activate the green character
        greenCharacter.SetActive(true);

        // 3) set the green character walking animation
        Animator greenAnimator = greenCharacter.GetComponent<Animator>();
        greenAnimator.SetTrigger("GreenWalkRight");
    }

    public void ShowRedCharacter()
    {
        // 1) deactivate the gray character
        grayCharacter.SetActive(false);

        // 2) activate the red character
        redCharacter.SetActive(true);

        // 3) set the red character walking animation
        Animator redAnimator = redCharacter.GetComponent<Animator>();
        redAnimator.SetTrigger("RedWalkLeft");
    }
}
