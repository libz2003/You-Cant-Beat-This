using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 10;

    void Start()
    {
        Lives = startLives;
    }
}
