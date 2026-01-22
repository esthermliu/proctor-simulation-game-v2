using UnityEngine;

public class SimpleEntranceMotion : MonoBehaviour
{
    public float duration = 1.2f;      // total time to move in
    public float startOffsetX = -3f;   // how far left to start
    public float stopX = 0f;

    public float bobAmplitude = 0.08f;
    public float bobFrequency = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float t = 0f;
    private bool entering = true;

    void Start()
    {
        endPos = transform.position;
        startPos = endPos + Vector3.right * startOffsetX;
        transform.position = startPos;
    }

    void Update()
    {
        if (!entering) return;

        t += Time.deltaTime / duration;
        t = Mathf.Clamp01(t);

        // Smooth easing (SmoothStep)
        float easedT = Mathf.SmoothStep(0f, 1f, t);

        // Horizontal movement
        Vector3 pos = Vector3.Lerp(startPos, endPos, easedT);

        // Bob fades out as movement ends
        float bobFade = 1f - easedT;
        float bob = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude * bobFade;

        transform.position = pos + Vector3.up * bob;

        if (t >= 1f)
        {
            transform.position = endPos;
            entering = false;
        }
    }
}
