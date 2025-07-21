using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOnAnyInput : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public string scene = "NomeDaCena";
    private bool isFading = false;

    void Update()
    {
        if (!isFading && Input.anyKeyDown)
        {
            StartCoroutine(FadeAndSwitchScene());
        }
    }

    IEnumerator FadeAndSwitchScene()
    {
        isFading = true;
        yield return StartCoroutine(Fade(0f, 1f)); // Fade para preto
        SceneManager.LoadScene(scene);       // Muda de cena
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
