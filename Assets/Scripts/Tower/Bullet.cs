using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 700.0f;
    public GameObject bulletImpactEffect;

    public void Seek(Transform _target)
    {
        direction = (_target.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        Destroy(gameObject, 10.0f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitTarget(other.gameObject);
        }
    }
    void HitTarget(GameObject target)
    {
        GameObject effectInstance = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        target.GetComponent<EnemyAndPath.EnemyHealth>().TakeDamage(1);
        Destroy(effectInstance, 2.0f);
    }
}
