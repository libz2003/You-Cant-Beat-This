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
    public AudioClip introduction;
    public AudioClip ending;
    public AudioClip sellTower;

    public float minInterval = 30f;
    public float maxInterval = 60f;

    [System.Serializable]
    public class CaptionedClip
    {
        public AudioClip clip;
        [TextArea]
        public string subtitle;
    }

    public CaptionedClip[] captionedClips;

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

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(RandomLoop());

        if (PersistentSettings.instance.playHint)
        {
            PlayHint();
        }
        else
        {
            PersistentSettings.instance.playHint = true;
        }
    }

    public static bool IsMainAudioPlaying()
    {
        return Instance != null && Instance.audioSource != null && Instance.audioSource.isPlaying;
    }

    IEnumerator RandomLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Wait until nothing is playing
            yield return new WaitUntil(() => !audioSource.isPlaying);

            if (randomSounds.Length > 0)
            {
                AudioClip clip = randomSounds[Random.Range(0, randomSounds.Length)];
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
                // Debug.Log("Clip played!");
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        // Debug.Log("Attempted to play sound");
        if (clip != null)
            StartCoroutine(PlaySFXCoroutine(clip));
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

    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        // Debug.Log("Entered coroutine");
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = clip;
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