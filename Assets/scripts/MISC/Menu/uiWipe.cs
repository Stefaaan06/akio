using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class uiWipe : MonoBehaviour
{
    public float wipeDuration = 1f; // Duration of the wipe effect in seconds
    public Vector2 startPosition; // The starting position of the panel (usually outside the screen)
    public Vector2 endPosition;   // The end position of the panel (final position after the wipe)

    private RectTransform rectTransform;
    private float elapsedTime;
    private bool isWiping = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartWipeIn();
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPosition;
    }

    public void StartWipeIn()
    {
        if (!isWiping)
        {
            elapsedTime = 0f;
            isWiping = true;
            StartCoroutine(WipeIn());
        }
    }

    public void StartWipeOut()
    {
        if (!isWiping)
        {
            elapsedTime = 0f;
            isWiping = true;
            StartCoroutine(WipeOut());
        }
    }

    private System.Collections.IEnumerator WipeIn()
    {
        while (elapsedTime < wipeDuration)
        {
            float t = elapsedTime / wipeDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
        isWiping = false;
    }

    private System.Collections.IEnumerator WipeOut()
    {
        while (elapsedTime < wipeDuration)
        {
            float t = elapsedTime / wipeDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(endPosition, startPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = startPosition;
        isWiping = false;
    }
}
