using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;

    public TextMeshProUGUI subtitleText;
    public CanvasGroup canvasGroup;
    public float fadeTime = 0.25f;

    Coroutine currentRoutine;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        if (subtitleText != null)
        {
            subtitleText.text = "";
        }
    }

    public void ShowSubtitle(string text, float duration)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(ShowSubtitleRoutine(text, duration));
    }

    IEnumerator ShowSubtitleRoutine(string text, float duration)
    {
        if (subtitleText == null || canvasGroup == null)
        {
            yield break;
        }

        subtitleText.text = text;

        // fade in
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // stay visible
        yield return new WaitForSeconds(duration);

        // fade out
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = 0f;

        subtitleText.text = "";
        currentRoutine = null;
    }
}
