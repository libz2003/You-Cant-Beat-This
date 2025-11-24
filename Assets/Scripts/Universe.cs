
using UnityEngine;
using UnityEngine.SceneManagement;

public class Universe : MonoBehaviour
{
    public static Universe instance;
    public string LoseScene;
    public string WinScene;

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
        SceneManager.LoadScene(LoseScene);
    }

    public void Win()
    {
        SceneManager.LoadScene(WinScene);
    }
}
