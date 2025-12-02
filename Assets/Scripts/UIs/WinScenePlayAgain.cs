using UnityEngine;

public class WinScenePlayAgain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PersistentSettings.instance.numberBugRemaining() == 0)
        {
            Destroy(gameObject);
        }
    }
}
