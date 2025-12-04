using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject startScreenPanel;
    public CanvasGroup startScreenCanvasGroup;
    public Button startButton;

    [Header("Fade Settings")]
    public float fadeDuration = 0.8f; // seconds

    private bool gameStarted = false;

    void Awake()
    {
        // If we already showed the start screen once in this app run,
        // skip it entirely.
        if (Universe.startScreenShown)
        {
            Time.timeScale = 1f;

            if (startScreenPanel != null)
            {
                startScreenPanel.SetActive(false);
            }

            // Disable this script; it has nothing left to do
            enabled = false;
            return;
        }

        // First time: pause game and show the start screen
        Time.timeScale = 0f;

        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }

        if (startScreenCanvasGroup != null)
        {
            startScreenCanvasGroup.alpha = 1f;
            startScreenCanvasGroup.interactable = true;
            startScreenCanvasGroup.blocksRaycasts = true;
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }
    }

    void OnStartButtonPressed()
    {
        if (gameStarted) return;
        gameStarted = true;

        if (startButton != null)
        {
            startButton.interactable = false;
        }

        StartCoroutine(FadeOutAndStart());
        SoundEffectManager.PlayButton();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.introduction);
    }

    private IEnumerator FadeOutAndStart()
    {
        if (startScreenCanvasGroup == null)
        {
            // Fallback: no CanvasGroup assigned, just hide instantly
            if (startScreenPanel != null)
            {
                startScreenPanel.SetActive(false);
            }

            Universe.startScreenShown = true;
            Time.timeScale = 1f;
            yield break;
        }

        float elapsed = 0f;
        float startAlpha = startScreenCanvasGroup.alpha;
        float endAlpha = 0f;

        startScreenCanvasGroup.interactable = false;
        startScreenCanvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            // Use unscaled time so fade works while timeScale is 0
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            startScreenCanvasGroup.alpha = newAlpha;

            yield return null;
        }

        startScreenCanvasGroup.alpha = 0f;
        startScreenCanvasGroup.blocksRaycasts = false;

        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(false);
        }

        // Mark that we have shown it, so future loads skip it
        Universe.startScreenShown = true;

        // Now let the game run
        Time.timeScale = 1f;
    }
}
