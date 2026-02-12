using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class TitleScreenManager : MonoBehaviour
{
    [Header("Title References")]
    public GameObject titlePage;
    public GameObject titlePencilA;
    public GameObject titlePencilB;
    public GameObject titlePencilC;

    [Header("Controls References")]
    public GameObject controlsPage;
    public GameObject optionAInfo;
    public GameObject optionBInfo;
    public GameObject optionCInfo;

    public GameObject controlsPencilA;
    public GameObject controlsPencilB;
    public GameObject controlsPencilC;

    public GameObject controlsPaperEdge;

    [Header("Credits References")]
    public GameObject creditsPage;
    public GameObject creditsPaperEdge;

    // This function will be called when the button is clicked
    //public void PlayGame()
    //{
    //    FadeManager.Instance.FadeToScene("Scene1_Day1_V2");
    //}

    // ========================================
    //          NAVIGATION FUNCTIONS
    // ========================================

    // This function is called when the edge of the page is clicked for both
    // the CONTROLS and the CREDITS scene
    public void ReturnToTitle()
    {
        // Reset all Controls Pencil Hoverlays (doesn't matter if we are actually on controls scene)
        // AND any selected options
        ResetControls();
        ResetCredits();

        // Set all other "pages" (i.e., CREDITS and CONTROLS) to inactive
        controlsPage.SetActive(false);
        creditsPage.SetActive(false);

        // show the title screen page
        titlePage.SetActive(true);
    }

    // Called on click to Controls from title screen
    public void GoToControls()
    {
        // Make sure all pencil hoverlays are set as inactive in title screen first!
        ResetTitle();

        // Then set all other pages to inactive
        titlePage.SetActive(false);
        creditsPage.SetActive(false);

        controlsPage.SetActive(true);
    }

    // Called on click to Credits from title screen
    public void GoToCredits()
    {
        // Make sure all pencil hoverlays are set as inactive in title screen first!
        ResetTitle();

        titlePage.SetActive(false);
        controlsPage.SetActive(false);

        creditsPage.SetActive(true);
    }


    private void ResetTitle()
    {
        // reset the pencil hoverlays
        titlePencilA.SetActive(false);
        titlePencilB.SetActive(false);
        titlePencilC.SetActive(false);
    }

    private void ResetControls()
    {
        // reset the pencil hoverlays
        controlsPencilA.SetActive(false);
        controlsPencilB.SetActive(false);
        controlsPencilC.SetActive(false);

        // reset the options shown
        optionAInfo.SetActive(false);
        optionBInfo.SetActive(false);
        optionCInfo.SetActive(false);

        // reset the edge of the paper
        controlsPaperEdge.SetActive(false);
    }

    private void ResetCredits()
    {
        // reset the edge of the paper
        creditsPaperEdge.SetActive(false);
    }

    // ========================================
    //          "CONTROLS" FUNCTIONS
    // ========================================
    public void OnControlOptionClick(int optionNum) {
        if (optionNum == 1) {
            // Set all other option info to inactive
            optionBInfo.SetActive(false);
            optionCInfo.SetActive(false);

            // Show option A info
            optionAInfo.SetActive(true);
        } else if (optionNum == 2)
        {
            // Set all other option info to inactive
            optionAInfo.SetActive(false);
            optionCInfo.SetActive(false);

            // Show option B info
            optionBInfo.SetActive(true);
        } else if (optionNum == 3)
        {
            // Set all other option info to inactive
            optionAInfo.SetActive(false);
            optionBInfo.SetActive(false);

            // Show option C info
            optionCInfo.SetActive(true);
        } 
    }


}
