using UnityEngine;

public class SupervisorPause3Manager : MonoBehaviour
{
    [SerializeField] private SupervisorSpeechManager speechManager;

    [Header("Pause 3 objects")]
    public GameObject smallMaterials;
    public GameObject smallExamGuide;

    public GameObject largeMaterials;
    public GameObject largeExamGuide;

    public TutorialClickable smallMaterialsClickable;
    public TutorialClickable smallExamGuideClickable;

    // keep track of which items have been returned, so know when to move on
    private bool materialsReturned = false;
    private bool examGuideReturned = false;

    // keep track of when pause 3 is done
    private bool completedPause3 = false;

    public void MaterialsReturned()
    {
        materialsReturned = true;
        CheckAllItemsReturned();
    }

    public void ExamGuideReturned()
    {
        examGuideReturned = true;
        CheckAllItemsReturned();
    }

    // show the button once all items have been clicked
    public void CheckAllItemsReturned()
    {
        // don't show button if no longer in phase 3
        if (completedPause3) return;

        if (materialsReturned && examGuideReturned)
        {
            CompletePause3();
        }
    }

    // function to be called onclick by the inspection button
    public void CompletePause3()
    {
        if (completedPause3) return;

        completedPause3 = true;

        // 1) Hide the enlarged versions of the objects
        largeMaterials.SetActive(false);
        largeExamGuide.SetActive(false);

        // 2) Show the small versions
        smallMaterials.SetActive(true);
        smallExamGuide.SetActive(true);

        // 3) Ensure that all small versions can no longer be clicked
        smallMaterialsClickable.SetClickable(false);
        smallExamGuideClickable.SetClickable(false);

        // 4) Ensure that pause 3 is no longer ongoing for all small versions
        smallMaterialsClickable.SetPause3(false);
        smallExamGuideClickable.SetPause3(false);

        // continue with dialogue
        speechManager.ResumeDialogue();
    }
}
