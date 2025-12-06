using UnityEngine;

public class TreeCollisionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PersistentSettings.instance.treeCuttable)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Tree Collision detected");

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("GunBullet"))
        {
            // TODO: audio
        }

        if (collision.gameObject.CompareTag("Enemy") && PersistentSettings.instance.treeCuttable)
        {
            //PersistentSettings.instance.targetTreeCuttable = false;
            //AudioManager.Instance.PlaySFX(AudioManager.Instance.treeReaction, true);
        }
    }
}
