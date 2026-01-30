using UnityEngine;

public class StudentAnimationController : MonoBehaviour
{
    private Animator animator;
    public GameObject nextStudent;

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
        if (nextStudent != null)
        {
            nextStudent.SetActive(true);
        }
    }
}
