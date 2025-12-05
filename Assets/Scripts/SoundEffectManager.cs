
using UnityEngine;

/**
 * Only sound effects, not narration,
 * bullet, enemy etc. 
 */
public class SoundEffectManager: MonoBehaviour
{
    private static SoundEffectManager instance;
    private AudioSource audioSource;
    
    // public AudioClip enemyHurt; // doesnt sound good
    public AudioClip explosion;
    public AudioClip gunShoot;
    public AudioClip gearShoot;
    public AudioClip bankExplosion;
    public AudioClip buttonClick;
    public AudioClip towerBuild;
    public AudioClip skeleDeath;
    private float targetVolume = 1.0f;
    private float volume = 1.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        audioSource = GetComponent<AudioSource>();
    }

    // public static void PlayEnemyHurt()
    // {
    //     instance.audioSource.PlayOneShot(instance.enemyHurt);
    // }

    void Update()
    {
        if (AudioManager.IsMainAudioPlaying())
        {
            targetVolume = 0.2f;
        }
        else
        {
            targetVolume = 1.0f;
        }

        volume = Mathf.Lerp(volume, targetVolume, Time.deltaTime * 5f);
    }

    public static void PlayTowerBuild()
    {
        instance.audioSource.PlayOneShot(instance.towerBuild, 2.0f * instance.volume);
    }
    public static void PlaySkeleDeath()
    {
        instance.audioSource.PlayOneShot(instance.skeleDeath, 1.0f * instance.volume);
    }

    public static void PlayButton()
    {
        instance.audioSource.PlayOneShot(instance.buttonClick, 1.0f * instance.volume);
    }

    public static void PlayBankExplosion()
    {
        instance.audioSource.PlayOneShot(instance.bankExplosion, 1.0f * instance.volume);
    }

    public static void PlayExplosion()
    {
        instance.audioSource.PlayOneShot(instance.explosion, 0.5f * instance.volume);
    }

    public static void PlayGunShoot()
    {
        instance.audioSource.PlayOneShot(instance.gunShoot, 0.1f * instance.volume);
    }

    public static void PlayGearShoot()
    {
        instance.audioSource.PlayOneShot(instance.gearShoot, 1.0f * instance.volume);
    }

}