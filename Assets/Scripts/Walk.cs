using UnityEngine;

public class PathWalker : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] waypoints;
    public float moveSpeed=25.0f;
    public float turnSpeed=5.0f;
    [Header("Animation")]
    public Animator animator;
    private int currentPointIndex=0;
    void Start()
    {
        if(animator!=null)
            animator.SetBool("IsWalking",true);
    }
    void Update()
    {
        if(waypoints.Length==0)
            return;
        Transform target=waypoints[currentPointIndex];
        transform.position=Vector3.MoveTowards(transform.position,target.position,moveSpeed*Time.deltaTime);
        Vector3 direction=(target.position-transform.position).normalized;
        if(direction!=Vector3.zero)
        {
            Quaternion lookRotation=Quaternion.LookRotation(direction);
            transform.rotation=Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*turnSpeed);
        }
        if(Vector3.Distance(transform.position,target.position)<0.1f)
        {
            currentPointIndex++;
            if(currentPointIndex>=waypoints.Length)
                currentPointIndex=0;
        }
    }
}
