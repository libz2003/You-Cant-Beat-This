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
        if (collision.gameObject.CompareTag("enemy"))
        {
            // TODO: audio
            PersistentSettings.instance.targetOptionObstacle = false;
        }
    }
}
