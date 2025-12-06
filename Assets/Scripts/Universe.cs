using UnityEngine;
using UnityEngine.SceneManagement;

public class Universe : MonoBehaviour
{
    public static Universe instance;

    // Has the start screen already been shown in this app run?
    public static bool startScreenShown = false;

    public string LoseScene;
    public string WinScene;
    public string MainScene;
    public string TestAgainScene;

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
        if (PersistentSettings.instance.numberBugRemaining() <= 1)
        {
            // win
            SceneManager.LoadScene(WinScene);
        }
        else
        {
            SceneManager.LoadScene(TestAgainScene);
        }
    }

    public void RestartWithoutFixBug()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainScene);
    }

    public void Restart()
    {
        if (PersistentSettings.instance.canPlaceOnPath != PersistentSettings.instance.targetCanPlaceOnPath)
        {
            // TODO: audio
            PersistentSettings.instance.canPlaceOnPath = PersistentSettings.instance.targetCanPlaceOnPath;
        }
        if (PersistentSettings.instance.optionObstacle != PersistentSettings.instance.targetOptionObstacle)
        {
            // TODO: audio
            PersistentSettings.instance.optionObstacle = PersistentSettings.instance.targetOptionObstacle;
        }
        if (PersistentSettings.instance.sellOption != PersistentSettings.instance.targetSellOption)
        {
            // TODO: audio
            PersistentSettings.instance.sellOption = PersistentSettings.instance.targetSellOption;
        }
        if (PersistentSettings.instance.bankBreakable != PersistentSettings.instance.targetBankBreakable)
        {
            // TODO: audio
            PersistentSettings.instance.bankBreakable = PersistentSettings.instance.targetBankBreakable;
        }
        if (PersistentSettings.instance.treeCuttable != PersistentSettings.instance.targetTreeCuttable)
        {
            // TODO: audio
            PersistentSettings.instance.treeCuttable = PersistentSettings.instance.targetTreeCuttable;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainScene);
    }
}
