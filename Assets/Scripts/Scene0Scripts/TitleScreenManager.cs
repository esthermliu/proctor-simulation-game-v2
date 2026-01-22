using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class TitleScreenManager : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene1_Day1"); // Name of your next scene
    }
}
