using UnityEngine;

public class SignatureAppear : MonoBehaviour
{
    public GameObject endDay;

    // OnClick event for the button
    public void OnSignatureButtonClick()
    {
        // Make signature show up
        gameObject.SetActive(true);

        // Show the clock out button
        endDay.SetActive(true);

        EventLogger.Log(new GameEvent {
            eventTypeEnum = EventType.eval_signed,
        });
    }

}
