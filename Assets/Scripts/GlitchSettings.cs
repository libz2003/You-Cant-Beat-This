using UnityEngine;

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
        isSettingsOpen = !isSettingsOpen;
        if(settingsPanel != null)
            settingsPanel.SetActive(isSettingsOpen);
    }
}
