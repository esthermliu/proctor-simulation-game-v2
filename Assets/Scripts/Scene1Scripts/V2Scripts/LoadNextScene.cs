using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string scene2Name = "Scene2_Day1_V2";

    public void LoadScene()
    {
        //// make the last character inactive
        //gameObject.SetActive(false);

        // load next scene
        SceneManager.LoadScene(scene2Name);
    }
}
