using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialDraggable : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [Header("Link to the small version")]
    public GameObject smallPaper;

    [Header("Link to Supervisor Pause 1 Manager (if ID card)")]
    public SupervisorPause1Manager supervisorPause1Manager;

    [Header("Link to Supervisor Pause 2 Manager (ONLY Day 1)")]
    public SupervisorPause2Manager supervisorPause2Manager;

    [Header("Link to Supervisor Pause 3 Manager (ONLY Day 1)")]
    public SupervisorPause3Manager supervisorPause3Manager;

    [Header("Link to Supervisor DAY 2 Pause 2 Manager (ONLY DAY 2)")]
    public SupervisorPause2Day2Manager supervisorPause2Day2Manager;

    [Header("Right-Clickable?")]
    public bool rightClickable = true;

    [Header("For Day 1 Tutorial")]
    public string whatAmI; // ID, ticket, or guide

    // keep track of which tutorial day we are on
    [Header("Which Day")]
    public bool day1 = false;

    [Header("Audio source (optional)")]
    public AudioSource audioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (rightClickable) {

            // 1) Check if player attempting to hide paper(right click or ctrl + click)
            bool rightClick = eventData.button == PointerEventData.InputButton.Right;
            bool ctrlClick = eventData.button == PointerEventData.InputButton.Left &&
                ((Input.GetKey(KeyCode.LeftControl)) || Input.GetKey(KeyCode.RightControl));

            if (rightClick || ctrlClick)
            {
                // if there is an audio source, play click sound
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }


                if (supervisorPause1Manager != null)
                {
                    // move the tutorial along
                    supervisorPause1Manager.CompletePause1();
                }

                if (supervisorPause2Day2Manager != null)
                {
                    // move the tutorial along once we have right clicked the external ticket
                    supervisorPause2Day2Manager.CompletePause2();
                }

                // FOR DAY 1 PAUSE 2 and PAUSE 3
                if (smallPaper != null &&
                    smallPaper.GetComponent<TutorialClickable>() != null &&
                    smallPaper.GetComponent<TutorialClickable>().IsDay1())
                {
                    TutorialClickable smallPaperTutorialClickable = smallPaper.GetComponent<TutorialClickable>();
                    if (smallPaperTutorialClickable.IsPause2())
                    {
                        if (whatAmI.Equals("ID"))
                        {
                            supervisorPause2Manager.IDCardReturned();
                        }
                        else if (whatAmI.Equals("ticket"))
                        {
                            supervisorPause2Manager.ExamTicketReturned();
                        }
                        else if (whatAmI.Equals("guide"))
                        {
                            supervisorPause2Manager.ExamGuideReturned();
                        }
                    } else if (smallPaperTutorialClickable.IsPause3())
                    {
                        if (whatAmI.Equals("materials"))
                        {
                            supervisorPause3Manager.MaterialsReturned();
                        }
                        else if (whatAmI.Equals("guide"))
                        {
                            supervisorPause3Manager.ExamGuideReturned();
                        }
                    }
                }

                // hide enlarged version
                gameObject.SetActive(false);

                // show smaller version
                if (smallPaper != null)
                {
                    smallPaper.SetActive(true);
                }
                else
                {
                    Debug.Log("Draggable.cs: Missing link to smaller version (ignore for the email notifications)");
                }

                return;
            }
        }
    
        // 2) Otherwise, we are clicking paper normally, so bring paper to FRONT
        transform.SetAsLastSibling();
    }


    public void TranslateToXPosition(float xPosition)
    {
        StopAllCoroutines();
        RectTransform rt = GetComponent<RectTransform>();
        StartCoroutine(TranslateCoroutine(new Vector3(xPosition, rt.anchoredPosition.y, 0)));
    }

    public void TranslateToYPosition(float yPosition)
    {
        StopAllCoroutines();
        RectTransform rt = GetComponent<RectTransform>();
        StartCoroutine(TranslateCoroutine(new Vector3(rt.anchoredPosition.x, yPosition, 0)));
    }

    private System.Collections.IEnumerator TranslateCoroutine(Vector3 endPos)
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 startPos = rt.anchoredPosition;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            rt.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.anchoredPosition = endPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Drag paper along with mouse
        transform.position += (Vector3)eventData.delta;
    }

    // enable right click once the tutorial on the item is complete
    public void SetRightClickable(bool rightClickable)
    {
        this.rightClickable = rightClickable;
    }

}
