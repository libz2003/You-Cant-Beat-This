using UnityEngine;
using System;

public class PathWalker : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 25.0f;
    public bool hitting = false;
    public float turnSpeed = 5.0f;
    public float maxDistanceFromPath = 1.0f;
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

    /**
     * Set the waypoints of this enemy, and reset walking stage.
     */
    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        currentPointIndex = 0;
        // rb.position = waypoints[0].position;  // I don't know why this doesn't work
        transform.position = waypoints[0].position;
    }
    
    void FixedUpdate()
    {
        if (hitting || waypoints.Length == 0) return;

        Transform target = waypoints[currentPointIndex];
        Vector3 direction = (target.position - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, target.position);

        // 1. Move via Velocity instead of Position
        // We check distance to avoid jittering when very close
        if (distance > Time.fixedDeltaTime*moveSpeed*2)
        {
            // Apply velocity to move toward target
            rb.linearVelocity = direction * moveSpeed;
            direction.y = 0;

            // Optional: Face the target
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed);

            // bounding to path
            if(currentPointIndex != 0) {
                Vector3 vecPath = waypoints[currentPointIndex].position - waypoints[currentPointIndex-1].position;
                Vector3 vecPathVert = Vector3.Dot(vecPath.normalized, rb.position - waypoints[currentPointIndex-1].position) * vecPath.normalized;
                rb.position = waypoints[currentPointIndex-1].position + vecPathVert;
            }
        }
        else
        {
            // Stop moving when close
            rb.linearVelocity = Vector3.zero;

            // Logic to switch to next waypoint
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                gameObject.SetActive(false);
                PlayerStats.Lives--;
                if (PlayerStats.Lives == 0) Universe.instance.GameOver();
            }
        }
    }
}
