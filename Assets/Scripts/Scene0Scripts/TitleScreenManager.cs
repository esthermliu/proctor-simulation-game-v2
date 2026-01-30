using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class TitleScreenManager : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void PlayGame()
    {
        FadeManager.Instance.FadeToScene("Scene1_Day1_V2");
        //SceneManager.LoadScene("Scene1_Day1_V2"); // Name of your next scene
    }
}
