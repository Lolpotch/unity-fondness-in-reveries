using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    public UnityEvent onFadeInEnd; // Exposed in Inspector
    public UnityEvent onFadeOutEnd; // Exposed in Inspector
    Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void FadeIn(float fadeDuration)
    {
        StartCoroutine(StartFade(0f, 1f, fadeDuration));
    }

    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(StartFade(1f, 0f, fadeDuration));
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