using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RemainingBugsUI : MonoBehaviour
{
    public TextMeshProUGUI remainingBugText;

    void Start()
    {
        remainingBugText.text = "The God of Bug tells you there is " + PersistentSettings.instance.numberBugRemaining().ToString() + " Bug remaining.";
    }
}
