using UnityEngine;
using EnemyAndPath;  // for EnemyHealth

public class GearBullet : MonoBehaviour
{
    private Vector3 direction;

    [Header("Stats")]
    public float speed = 700.0f;
    public int damage = 5;  // super powerful compared to normal bullet

    [Header("Effects")]
    public GameObject bulletImpactEffect;

    // Called by GearTower right after instantiation
    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, 1.0f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            return;
        }
        HitTarget();
    }

    private void HitTarget()
    {
        // if (bulletImpactEffect != null)
        // {
        //     GameObject effectInstance =
        //         Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        //     Destroy(effectInstance, 2.0f);
        // }
        //Destroy(gameObject);
    }
}
