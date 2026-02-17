using UnityEngine;

public class StudentAnimationController : MonoBehaviour
{
    private Animator animator;
    public GameObject nextStudent;
    public GameObject supervisorGrayCharacter;
    public GameObject clockOutButton;


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
        } else if (supervisorGrayCharacter != null) {
            // This should be the last student for the day
            // If there is a supervisor scene, then also spawn the supervisor

            // show the supervisor gray character
            supervisorGrayCharacter.SetActive(true);
        } else
        {
            // no supervisor scene, no next student, so show the clock out button
            clockOutButton.SetActive(true);
        }

    }
}
