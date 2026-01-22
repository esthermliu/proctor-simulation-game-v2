using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : MonoBehaviour
{
    public string scene2Name = "Scene2_Day1"; // Make sure this matches your scene name

    // This method is called by the Animation Event
    public void OnExitFinished()
    {
        // Load Scene 2
        Debug.Log("ON EXIT FINISHED CALLED");
        SceneManager.LoadScene(scene2Name);
    }
}
