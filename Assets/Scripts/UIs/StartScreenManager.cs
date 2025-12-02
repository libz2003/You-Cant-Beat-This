using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject startScreenPanel;
    public Button startButton;

    private bool gameStarted = false;

    void Awake()
    {
        // Freeze the game at the very beginning
        Time.timeScale = 0f;

        // Make sure panel is visible
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }

        // Hook up button event
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }
    }

    void OnStartButtonPressed()
    {
        if (gameStarted) return;

        gameStarted = true;

        // Hide start screen
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(false);
        }

        // Resume the game
        Time.timeScale = 1f;
    }
}
