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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && PersistentSettings.instance.targetOptionObstacle)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pauseReaction);
            PersistentSettings.instance.targetOptionObstacle = false;
        }
    }
}