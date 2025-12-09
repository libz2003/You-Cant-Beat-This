using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        PersistentSettings.instance.playThroughCount += 1;
        StartCoroutine(loadSceneAfterDelay(0.5f, LoseScene));
    }

    public void Win()
    {
        PersistentSettings.instance.playThroughCount += 1;
        SoundEffectManager.PlayWin();
        if (PersistentSettings.instance.numberBugRemaining() <= 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.ending, true);
            StartCoroutine(loadSceneAfterDelay(10.0f, WinScene));
        }
        else
        {
            StartCoroutine(loadSceneAfterDelay(1.0f, TestAgainScene));
        }
    }

    IEnumerator loadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }


    public void RestartWithoutFixBug()
    {
        PersistentSettings.instance.foundBug = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainScene);
    }

    public void Restart()
    {
        PersistentSettings.instance.foundBug = false;
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

        PersistentSettings.instance.playBugFixed = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(MainScene);
    }
}
