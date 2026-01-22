using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentAnimationController : MonoBehaviour
{
    private Animator animator;
    public GameObject nextStudent;
    public string scene2Name = "Scene2_Day1";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Admit()
    {
        animator.SetTrigger("Admit");
    }

    public void Deny()
    {
        animator.SetTrigger("Deny");
    }

    // CALLED BY ANIMATION EVENT
    public void OnExitAnimationFinished()
    {
        gameObject.SetActive(false);
        SpawnNextStudent();
    }


    public void SpawnNextStudent()
    {
        Debug.Log("SPAWNING NEXT STUDENT");
        if (nextStudent != null)
        {
            nextStudent.SetActive(true);

            // Trigger entrance animation
            Animator anim = nextStudent.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Entrance");
            }
        } else
        {
            // at this point, we load next scene
            Debug.Log("ON EXIT FINISHED CALLED");
            SceneManager.LoadScene(scene2Name);
        }
    }
}
