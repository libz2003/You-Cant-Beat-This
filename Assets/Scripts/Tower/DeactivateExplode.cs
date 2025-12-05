using UnityEngine;

public class DeactivateExplode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timeToDeactivate = 0.2f;
    void Start()
    {
        // deactivate collider after 0.2 seconds
        Invoke(nameof(DisableCollider), timeToDeactivate);
    }

    void DisableCollider()
    {
        Collider col = GetComponent<Collider>();
        col.enabled = false;
    }
}
