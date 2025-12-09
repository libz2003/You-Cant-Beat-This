using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip[] randomSounds; // Done
    public AudioClip bankHint;
    public AudioClip bankReaction; // Done
    public AudioClip towerHint;
    public AudioClip towerReaction; // Done
    public AudioClip treeHint;
    public AudioClip treeReaction; // Done
    public AudioClip settingsHint;
    public AudioClip settingsReaction; // Done
    public AudioClip pauseHint;
    public AudioClip pauseReaction; // Done
    public AudioClip introduction1;
    public AudioClip introduction2;
    public AudioClip ending;
    public AudioClip sellTower;
    public AudioClip killEnemy;
    public AudioClip loseLife;
    public AudioClip bugFixed;

    public float minInterval = 15f;
    public float maxInterval = 40f;

    [System.Serializable]
    public class CaptionedClip
    {
        public AudioClip clip;
        [TextArea]
        public string subtitle;
    }

    public CaptionedClip[] captionedClips;
    private AudioClip[] playQueue = new AudioClip[4];
    private int rightIndex = 0;
    private int leftIndex = 0;

    private string GetSubtitleForClip(AudioClip clip)
    {
        if (clip == null || captionedClips == null)
        {
            return null;
        }

        foreach (var entry in captionedClips)
        {
            if (entry != null && entry.clip == clip && !string.IsNullOrEmpty(entry.subtitle))
            {
                return entry.subtitle;
            }
        }

        return null;
    }


    public static bool IsMainAudioPlaying()
    {
        return Instance != null && Instance.audioSource != null && Instance.audioSource.isPlaying;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        restart();
    }

    public void restart()
    {
        StopAllCoroutines();
        playQueue = new AudioClip[4];
        rightIndex = 0;
        leftIndex = 0;

        if(PersistentSettings.instance.playBugFixed)
        {
            PlaySFX(bugFixed);
            PersistentSettings.instance.playBugFixed = false;
        }

        StartCoroutine(RandomLoop());

        if (PersistentSettings.instance.playHint)
        {
            StartCoroutine(PlayHintAfterWait(20.0f));
        }
        else
        {
            PersistentSettings.instance.playHint = true;
        }
    }

    IEnumerator PlayHintAfterWait(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (PersistentSettings.instance.playHint)
            PlayHint();
    }

    public bool tryPlayClip(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            PlayOneClip(clip);
            return true;
        }
        return false;
    }

    IEnumerator RandomLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Wait until nothing is playing
            yield return new WaitUntil(() => !audioSource.isPlaying);

            if (PlayerStats.Lives <= 2)
                break;

            if (randomSounds.Length > 0)
            {
                AudioClip clip = randomSounds[Random.Range(0, randomSounds.Length)];
                PlayOneClip(clip);
            }
        }
    }

    public void PlaySFX(AudioClip clip, bool interrupt = false)
    {
        if(interrupt)
        {
            PlayOneClip(clip); // no race determinacy because unity executes 1 update at a time
            playQueue = new AudioClip[4];
            rightIndex = 0;
            leftIndex = 0;
        }else
        {
            playQueue[rightIndex] = clip;
            rightIndex = (rightIndex + 1) % playQueue.Length;
            if(rightIndex == leftIndex) // queue full, drop oldest
                leftIndex = (leftIndex + 1) % playQueue.Length;
        }
    }


    void Update()
    {
        if (!audioSource.isPlaying && leftIndex != rightIndex)
        {
            AudioClip clip = playQueue[leftIndex];
            leftIndex = (leftIndex + 1) % playQueue.Length;
            PlayOneClip(clip);
        }
    }

    public void PlayHint()
    {
        if (PersistentSettings.instance.canPlaceOnPath)
            PlaySFX(towerHint);
        else if (PersistentSettings.instance.treeCuttable)
            PlaySFX(treeHint);
        else if (PersistentSettings.instance.sellOption)
            PlaySFX(settingsHint);
        else if (PersistentSettings.instance.optionObstacle)
            PlaySFX(pauseHint);
        else if (PersistentSettings.instance.bankBreakable)
            PlaySFX(bankHint);
    }

    private void PlayOneClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        if (SubtitleManager.instance != null)
        {
            string subtitle = GetSubtitleForClip(clip);
            if (!string.IsNullOrEmpty(subtitle))
            {
                SubtitleManager.instance.ShowSubtitle(subtitle, clip.length);
            }
        }
    }
}