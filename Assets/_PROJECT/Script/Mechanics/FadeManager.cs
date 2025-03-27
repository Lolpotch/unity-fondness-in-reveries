using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    public void FadeIn(Image[] FadeInImages, float fadeDuration)
    {
        foreach (Image img in FadeInImages)
        {
            StartCoroutine(FadeImage(img, 0f, 1f, fadeDuration));
        }
    }

    public void FadeOut(Image[] FadeOutImages, float fadeDuration)
    {
        foreach (Image img in FadeOutImages)
        {
            StartCoroutine(FadeImage(img, 1f, 0f, fadeDuration));
        }
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
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
    }
}
