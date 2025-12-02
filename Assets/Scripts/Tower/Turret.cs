using UnityEngine;
using EnemyAndPath;  // to access the Enemy component

public class Turret : MonoBehaviour
{
    private Transform target;
    private float fireCountdown = 0.0f;

    [Header("Attributes")]
    public float range = 150.0f;
    public float fireRate = 1.0f;
    public float rotationSpeed = 10.0f;
    public float tooCloseRange = 30.0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject selectedEnemy = null;
        float selectedEnemyDistance = Mathf.Infinity;
        int bestIndex = int.MaxValue;

        foreach (GameObject enemyGO in enemies)
        {
            // Make sure it has an Enemy component
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            if (enemy == null)
            {
                continue;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemyGO.transform.position);

            // Only consider enemies within range
            if (distanceToEnemy > range)
            {
                continue;
            }

            int enemyIndex = enemy.Index;

            // Pick the enemy with the smallest index in range
            if (enemyIndex < bestIndex)
            {
                bestIndex = enemyIndex;
                selectedEnemy = enemyGO;
                selectedEnemyDistance = distanceToEnemy;
            }
        }

        if (selectedEnemy != null)
        {
            // // exploit: if too close then enemy is destroyed
            // if (selectedEnemyDistance <= tooCloseRange)
            // {
            //     selectedEnemy.SetActive(false);
            //     // Destroy(selectedEnemy);
            //     target = null;
            // }
            // else
            // {
            //     target = selectedEnemy.transform;
            // }

            target = selectedEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        if (fireCountdown <= 0.0f)
        {
            Shoot();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // TODO: audio
            PersistentSettings.instance.targetCanPlaceOnPath = false;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
