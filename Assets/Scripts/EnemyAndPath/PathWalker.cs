using UnityEngine;
using System;

public class PathWalker : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 25.0f;
    public bool hitting = false;
    public float turnSpeed = 5.0f;
    public float maxDistanceFromPath = 5.0f;
    [Header("Animation")]
    public Animator animator;
    private int currentPointIndex = 0;
    private CharacterController characterController;

    // 0 = just spawned, 1 = reached/at the last waypoint.
    public float PathProgress
    {
        get
        {
            if (waypoints == null || waypoints.Length <= 1)
            {
                return 0f;
            }

            int maxIndex = Mathf.Max(1, waypoints.Length - 1);
            // This uses waypoint index only (not exact position between waypoints),
            // but it is enough to know who is further along the path.
            return Mathf.Clamp01((float)currentPointIndex / maxIndex);
        }
    }

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    /**
     * Set the waypoints of this enemy, and reset walking stage.
     */
    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        currentPointIndex = 0;
        transform.position = waypoints[0].position;
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentPointIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        direction = direction.normalized;
        float distance = Vector3.Distance(transform.position, target.position);

        // 1. Move via Velocity instead of Position
        // We check distance to avoid jittering when very close
        if (distance > Time.fixedDeltaTime*moveSpeed*2)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed);

            // change it to use character controller:
            characterController.Move(direction * moveSpeed * Time.fixedDeltaTime);

            // bounding to path
            if(currentPointIndex != 0) {
                Vector3 vecPath = waypoints[currentPointIndex].position - waypoints[currentPointIndex-1].position;
                Vector3 vecPathVert = Vector3.Dot(vecPath.normalized, transform.position - waypoints[currentPointIndex-1].position) * vecPath.normalized;
                Vector3 offset = (waypoints[currentPointIndex-1].position + vecPathVert) - transform.position;
                Vector3 offsetY = new Vector3(0, offset.y, 0);
                Vector3 offsetXZ = new Vector3(offset.x, 0, offset.z);
                characterController.Move(offsetY);
                if (offsetXZ.magnitude > maxDistanceFromPath)
                    characterController.Move(offsetXZ);
            }
        }
        else
        {
            // Logic to switch to next waypoint
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                currentPointIndex = waypoints.Length - 1;
            }
        }
    }


}
