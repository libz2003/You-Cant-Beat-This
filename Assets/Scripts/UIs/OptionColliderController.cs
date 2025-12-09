using UnityEngine;

public class OptionColliderController : MonoBehaviour
{
    void Start()
    {
        if (!PersistentSettings.instance.optionObstacle)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && PersistentSettings.instance.optionObstacle && PersistentSettings.instance.targetOptionObstacle)
        {
            PersistentSettings.instance.foundBug = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pauseReaction, true);
            PersistentSettings.instance.targetOptionObstacle = false;
            PersistentSettings.instance.playHint = false;
        }
    }
}