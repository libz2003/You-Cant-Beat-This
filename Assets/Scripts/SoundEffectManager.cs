
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

    private static bool CanPlaySfx()
    {
        // If there's no AudioManager, allow SFX.
        if (AudioManager.Instance == null)
            return true;

        // Block SFX while main audio is playing
        return !AudioManager.IsMainAudioPlaying();
    }

    public static void PlayTowerBuild()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.towerBuild, 2.0f);
    }

    public static void PlayButton()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.buttonClick);
    }

    public static void PlayBankExplosion()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.bankExplosion);
    }

    public static void PlayExplosion()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.explosion);
    }

    public static void PlayGunShoot()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.gunShoot, 0.1f);
    }

    public static void PlayGearShoot()
    {
        if (!CanPlaySfx()) return;
        instance.audioSource.PlayOneShot(instance.gearShoot);
    }

}