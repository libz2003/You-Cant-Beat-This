using UnityEngine;

public class TreeTipCollisionManager : MonoBehaviour
{
    void Start()
    {
        if (!PersistentSettings.instance.treeCuttable)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tree Tip Collision detected");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            PersistentSettings.instance.targetTreeCuttable = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.treeReaction);
        }
    }
}
