using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAgainFromLose : MonoBehaviour
{
    public void Restart()
    {
        Universe.instance.RestartWithoutFixBug();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Restart();
        }
    }
}
