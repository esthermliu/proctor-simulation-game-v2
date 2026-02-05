using UnityEngine;

public class Student : MonoBehaviour
{
    [Header("Bad Behavior (red) OR Question (green)")]
    public GameObject indicator;

    [Header("Investigation Link")]
    public GameObject investigate;

    [Header("Question Links")]
    public GameObject question;
    public GameObject guide;
    public GameObject goodResponse;
    public GameObject badResponse;

    [Header("Explanation Manager")]
    public ExplanationManager explanationManager;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // ========== SHOW Methods ==========
    public void ShowIndicator()
    {
        indicator.SetActive(true);

        // trigger the behavior animation
        PlayBehaviorAnimation();
    }

    public void ShowInvestigate()
    {
        investigate.SetActive(true);
    }

    public void ShowExplanation()
    {
        explanationManager.StartExplanation();
    }

    public void ShowQuestion()
    {
        question.SetActive(true);
    }

    public void ShowGuide()
    {
        guide.SetActive(true);
    }

    public void ShowGoodResponse()
    {
        goodResponse.SetActive(true);
    }

    public void ShowBadResponse()
    {
        badResponse.SetActive(true);
    }


    // ========== HIDE Methods ==========
    public void HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void HideInvestigate()
    {
        investigate.SetActive(false);
    }

    public void HideQuestion()
    {
        question.SetActive(false);
    }

    public void HideGuide()
    {
        guide.SetActive(false);
    }

    public void HideGoodResponse()
    {
        goodResponse.SetActive(false);
    }

    public void HideBadResponse()
    {
        badResponse.SetActive(false);
    }


    // Play the animation corresponding to the behavior
    public void PlayBehaviorAnimation()
    {
        animator.SetTrigger("StartBehavior");
    }
}
