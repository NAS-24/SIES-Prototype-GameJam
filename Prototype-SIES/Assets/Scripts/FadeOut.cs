using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 2f;   // time to fully fade
    public float delayBeforeFade = 1f; // wait time before fade starts
    private Image blackScreen;

    void Start()
    {
        blackScreen = GetComponent<Image>();
        if (blackScreen == null)
        {
            Debug.LogError("FadeOut: No Image component found!");
        }
    }

    public void StartFade()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        // wait before fading
        yield return new WaitForSeconds(delayBeforeFade);

        Color color = blackScreen.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            color.a = alpha;
            blackScreen.color = color;
            yield return null;
        }

        // ensure final alpha = 1 (fully black)
        color.a = 1f;
        blackScreen.color = color;
    }
}
