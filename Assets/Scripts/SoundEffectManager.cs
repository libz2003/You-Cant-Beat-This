
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

    public static void PlayExplosion()
    {
        instance.audioSource.PlayOneShot(instance.explosion);
    }

    public static void PlayGunShoot()
    {
        instance.audioSource.PlayOneShot(instance.gunShoot);
    }

    public static void PlayGearShoot()
    {
        instance.audioSource.PlayOneShot(instance.gearShoot);
    }

}