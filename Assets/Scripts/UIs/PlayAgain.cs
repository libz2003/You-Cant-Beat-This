using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAgain : MonoBehaviour
{
    public void Restart()
    {
        Universe.instance.Restart();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Restart();
        }
    }
}
