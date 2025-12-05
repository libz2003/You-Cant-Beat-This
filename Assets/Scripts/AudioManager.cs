using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip[] randomSounds; // Done
    public AudioClip bankReaction; // Done
    public AudioClip towerReaction; // Done
    public AudioClip treeReaction; // Done
    public AudioClip settingsReaction; // Done
    public AudioClip pauseReaction; // Done
    public AudioClip introduction;
    public AudioClip ending;

    public float minInterval = 30f;
    public float maxInterval = 60f;

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
                audioSource.clip = randomSounds[Random.Range(0, randomSounds.Length)];
                audioSource.Play();
                Debug.Log("Clip played!");
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log("Attempted to play sound");
        if (clip != null)
            StartCoroutine(PlaySFXCoroutine(clip));
    }

    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        Debug.Log("Entered coroutine");
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = clip;
        audioSource.Play();
    }
}