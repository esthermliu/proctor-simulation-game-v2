using UnityEngine;

public class Student : MonoBehaviour
{
    public GameObject indicator;
    public GameObject investigate;
    public GameObject explanation;
    public GameObject question;
    public GameObject guide;
    public GameObject goodResponse;
    public GameObject badResponse;

    // SHOW Methods
    public void ShowIndicator()
    {
        indicator.SetActive(true);
    }

    public void ShowInvestigate()
    {
        investigate.SetActive(true);
    }

    public void ShowExplanation()
    {
        explanation.SetActive(true);
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


    // HIDE Methods
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
}
