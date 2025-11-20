using UnityEngine;

public class Universe : MonoBehaviour
{
    public static Universe instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
