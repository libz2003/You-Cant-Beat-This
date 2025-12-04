using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Credits : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject PanelCred;
    public void OpenCred()
    {
        SoundEffectManager.PlayButton();
        PanelCred.SetActive(true);
    }
    public void CloseCred()
    {
        SoundEffectManager.PlayButton();
        PanelCred.SetActive(false);
    }
}
