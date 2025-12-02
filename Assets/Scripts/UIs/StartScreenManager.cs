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
        // Freeze the game at the very beginning
        Time.timeScale = 0f;

        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }

        if (startScreenCanvasGroup != null)
        {
            // Ensure it's fully visible at start
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

        // Disable further clicks on the button immediately
        if (startButton != null)
        {
            startButton.interactable = false;
        }

        // Start fade-out animation
        StartCoroutine(FadeOutAndStart());
    }

    private IEnumerator FadeOutAndStart()
    {
        if (startScreenCanvasGroup == null)
        {
            // Fallback: if we forgot to assign the CanvasGroup, just hide instantly
            if (startScreenPanel != null)
            {
                startScreenPanel.SetActive(false);
            }
            Time.timeScale = 1f;
            yield break;
        }

        float elapsed = 0f;
        float startAlpha = startScreenCanvasGroup.alpha;
        float endAlpha = 0f;

        // Make sure panel blocks clicks while fading out
        startScreenCanvasGroup.interactable = false;
        startScreenCanvasGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            // Use unscaled time so this works while Time.timeScale = 0
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            startScreenCanvasGroup.alpha = newAlpha;

            yield return null;
        }

        // Fully invisible now
        startScreenCanvasGroup.alpha = 0f;

        // Let gameplay clicks go through
        startScreenCanvasGroup.blocksRaycasts = false;

        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(false);
        }

        // Finally, start the game
        Time.timeScale = 1f;
    }
}
