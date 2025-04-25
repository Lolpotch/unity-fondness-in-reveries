using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeImage : MonoBehaviour
{
    [Range(0f, 1f)] public float alphaStart;
    public UnityEvent onFadeInEnd; // Exposed in Inspector
    public UnityEvent onFadeOutEnd; // Exposed in Inspector
    Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        Color color = image.color;
        image.color = new Color(color.r, color.g, color.b, alphaStart);
    }

    public void FadeIn(float fadeDuration)
    {
        StartCoroutine(StartFade(0f, 1f, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(StartFade(1f, 0f, fadeDuration));
    }

    public void FadeInOut(float fadeDuration)
    {
        StartCoroutine(FadeInOutRoutine(fadeDuration));
    }

    private IEnumerator FadeInOutRoutine(float fadeDuration)
    {
        yield return StartCoroutine(StartFade(0f, 1f, fadeDuration)); // Fade in
        yield return StartCoroutine(StartFade(1f, 0f, fadeDuration)); // Then fade out
    }

    private IEnumerator StartFade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        
        image.color = new Color(color.r, color.g, color.b, endAlpha);

        if (onFadeInEnd != null && startAlpha == 0f)
        {
            onFadeInEnd.Invoke(); // Executes assigned function(s)
        }

        if (onFadeOutEnd != null && startAlpha == 1f)
        {
            onFadeOutEnd.Invoke(); // Executes assigned function(s)
        }
    }
}