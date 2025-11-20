using UnityEngine;

public class PathWalker : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 25.0f;
    public bool hitting = false;
    public float turnSpeed = 5.0f;
    [Header("Animation")]
    public Animator animator;
    private int currentPointIndex = 0;
    private Rigidbody rb;
    void Start()
    {
        if (animator != null)
            animator.SetBool("IsWalking", true);
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (hitting || waypoints.Length == 0) return;

        Transform target = waypoints[currentPointIndex];
        Vector3 direction = (target.position - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, target.position);

        // 1. Move via Velocity instead of Position
        // We check distance to avoid jittering when very close
        if (distance > 1.0f)
        {
            // Apply velocity to move toward target
            rb.linearVelocity = direction * moveSpeed;
            direction.y = 0;

            // Optional: Face the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed);
        }
        else
        {
            // Stop moving when close
            rb.linearVelocity = Vector3.zero;

            // Logic to switch to next waypoint
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                Destroy(gameObject);
                PlayerStats.Lives--;
                if (PlayerStats.Lives == 0) Universe.instance.GameOver();
            }
        }
    }
}
