using UnityEngine;

public class GlitchSettings : MonoBehaviour
{
    public GameObject visualMenu;

    public GameObject invisibleWall;

    private bool isSettingsOpen = false;

    void Start()
    {
        if(visualMenu != null) visualMenu.SetActive(false);
        if(invisibleWall != null) invisibleWall.SetActive(false);
    }

    public void ToggleSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        if(visualMenu != null) visualMenu.SetActive(isSettingsOpen);
        if(invisibleWall != null) invisibleWall.SetActive(isSettingsOpen);
    }
}
