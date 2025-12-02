using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RemainingBugsUI : MonoBehaviour
{
    public TextMeshProUGUI remainingBugText;

    void Start()
    {
        remainingBugText.text = "The God of Bug tells you there are " + PersistentSettings.instance.numberBugRemaining().ToString() + " Bugs remaining.";
    }
}
