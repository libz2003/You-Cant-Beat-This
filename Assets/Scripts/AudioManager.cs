using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] randomSounds;
    public AudioClip buttonClick;
    public AudioClip shootTower;
    public AudioClip introduction;
    public AudioClip ending;
    public AudioClip randomLines;
    public AudioClip bankBug;
    public AudioClip pauseBug;
    public AudioClip treeBug;
    public AudioClip pathBug;
    public AudioClip settingsBug;

    public float minInterval = 45f;
    public float maxInterval = 75f;

    void Start()
    {
        StartCoroutine(RandomLoop());
    }

    IEnumerator RandomLoop()
    {
        while (true)
        {
            // Wait a random time (around 1 minute)
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Pick and play a random clip
            if (randomSounds.Length > 0)
            {
                AudioClip clip = randomSounds[Random.Range(0, randomSounds.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayButtonClick() => PlaySFX(buttonClick);
}