using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Credits : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject PanelCred;
    public void OpenCred()
    {
        PanelCred.SetActive(true);
    }
    public void CloseCred()
    {
        PanelCred.SetActive(false);
    }
}
