using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIClickSquash : MonoBehaviour, IPointerClickHandler
{
    public RectTransform characterRT; // Character to squash

    private Vector3 originalScale;
    private Vector3 originalPosition;

    public float squashAmountX = 0.8f;
    public float squashDuration = 0.08f;

    public float bounceAmountX = 1.05f;
    public float bounceDuration = 0.05f;

    public float wiggleDistance = 5f;
    public int wiggleCount = 2;
    public float wiggleDuration = 0.03f;

    void Start()
    {
        originalScale = characterRT.localScale;
        originalPosition = characterRT.anchoredPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(SquashX());
    }

    private IEnumerator SquashX()
    {
        // 1️⃣ Squash thin
        characterRT.localScale = new Vector3(originalScale.x * squashAmountX, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(squashDuration);

        // 2️⃣ Bounce
        characterRT.localScale = new Vector3(originalScale.x * bounceAmountX, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(bounceDuration);

        // 3️⃣ Return scale
        characterRT.localScale = originalScale;

        // 4️⃣ Wiggle
        for (int i = 0; i < wiggleCount; i++)
        {
            characterRT.anchoredPosition = originalPosition + new Vector3(wiggleDistance, 0, 0);
            yield return new WaitForSeconds(wiggleDuration);

            characterRT.anchoredPosition = originalPosition + new Vector3(-wiggleDistance, 0, 0);
            yield return new WaitForSeconds(wiggleDuration);
        }

        // 5️⃣ Reset position
        characterRT.anchoredPosition = originalPosition;
    }
}