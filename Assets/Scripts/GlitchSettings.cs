using UnityEngine;
using UnityEngine.SceneManagement;
public class GlitchSettings : MonoBehaviour
{
    public GameObject settingsPanel;

    private bool isSettingsOpen = false;

    void Start()
    {
        if(settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    public void ToggleSettings()
    {
        SoundEffectManager.PlayButton();
        isSettingsOpen = !isSettingsOpen;
        if (settingsPanel != null)
            settingsPanel.SetActive(isSettingsOpen);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void RestartLevel()
    {
        SoundEffectManager.PlayButton();
        Time.timeScale = 1f;
        AudioManager.Instance.PlayHint();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
