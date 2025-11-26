using UnityEngine;

public class GearTower : MonoBehaviour
{
    private float fireCountdown = 0.0f;
    private bool hasTargetInRange = false;

    [Header("Attributes")]
    public float range = 150.0f;
    public float fireRate = 1.0f;           // shots per second
    public float rotationSpeed = 90.0f;     // spin speed of the tower
    public int bulletsPerShot = 12;         // how many bullets in the circle

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;         // prefab with GearBullet on it
    public Transform firePoint;             // where bullets spawn from

    void Start()
    {
        // Periodically check if there is at least one enemy in range
        InvokeRepeating(nameof(UpdateHasTarget), 0.0f, 0.3f);
    }

    void Update()
    {
        // Constantly spin for visual flair
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (!hasTargetInRange)
        {
            return;
        }

        if (fireCountdown <= 0.0f)
        {
            ShootCircle();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateHasTarget()
    {
        hasTargetInRange = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemyGO in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyGO.transform.position);
            if (distance <= range)
            {
                hasTargetInRange = true;
                break;
            }
        }
    }

    void ShootCircle()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("GearTower: bulletPrefab or firePoint not assigned.");
            return;
        }

        float angleStep = 360.0f / bulletsPerShot;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float angleDeg = i * angleStep;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            // Direction on the XZ plane
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), 0.0f, Mathf.Sin(angleRad));
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);

            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, rot);
            GearBullet bullet = bulletGO.GetComponent<GearBullet>();
            if (bullet != null)
            {
                bullet.Init(dir);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
