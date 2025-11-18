// using System;
// using UnityEngine;
//
// namespace EnemyAndPath
// {
//     public class Enemy : MonoBehaviour
//     {
//         // [SerializeField] private EnemyData data;
//         public static event Action<Enemy> OnEnemyReachedEnd;
//
//         private Path _currentPath;
//
//         private Vector3 _targetPosition;
//         private int _currentWaypoint;
//
//         public float speed = 1f;
//
//         private float time = 0f;
//
//         private void Awake()
//         {
//             _currentPath = GameObject.Find("MapPath").GetComponent<Path>();
//         }
//
//         private void OnEnable()
//         {
//             _currentWaypoint = 0;
//             _targetPosition = _currentPath.GetPosition(_currentWaypoint);
//             time = 0f;
//         }
//
//         void Update()
//         {
//             // // move towards target position
//             // transform.position = Vector3.MoveTowards(transform.position, _targetPosition, data.speed * Time.deltaTime);
//             //
//             // // when target reached, set new target position
//             // float relativeDistance = (transform.position - _targetPosition).magnitude;
//             // if (relativeDistance < 0.1f)
//             // {
//             //     if (_currentWaypoint < _currentPath.waypoints.Length - 1)
//             //     {
//             //         _currentWaypoint++;
//             //         _targetPosition = _currentPath.GetPosition(_currentWaypoint);
//             //     }
//             //     else // reached last waypoint
//             //     {
//             //         OnEnemyReachedEnd?.Invoke(this);
//             //         gameObject.SetActive(false);
//             //     }
//             // }
//             time += Time.deltaTime;
//             transform.position = _currentPath.PositionAt(time * speed);
//             
//             var remainingDistance = (transform.position - _currentPath.EndPosition).magnitude;
//         }
//     }
// }
