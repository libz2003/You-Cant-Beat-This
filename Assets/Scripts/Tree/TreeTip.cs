using UnityEngine;

public class TreeTip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
            Collider col = transform.parent.GetComponent<Collider>(); 
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            col.isTrigger = false;
            PersistentSettings.instance.targetTreeCuttable = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.treeReaction, true);
        }
    }
}
