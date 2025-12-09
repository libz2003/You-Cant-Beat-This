using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 700.0f;
    public int damage = 10;
    public GameObject bulletImpactEffect;

    [Header("Sound Effect")]
    public bool playExplosionSound = false;

    public void Seek(Transform _target)
    {
        direction = (_target.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        Destroy(gameObject, (_target.position - transform.position).magnitude / speed + 50.0f / speed);
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
        if (other.gameObject.CompareTag("Tree"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * 200.0f);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitTarget();
        }
    }   

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            return;
        }
        HitTarget();
    }
    void HitTarget()
    {
        if (bulletImpactEffect != null)
        {
            GameObject effectInstance = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 2.0f);
            
        }
        
        if (playExplosionSound)
            SoundEffectManager.PlayExplosion();  // sound effect
        
        Destroy(gameObject);
    }
}
