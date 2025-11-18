// using UnityEngine;
//
// namespace EnemyAndPath
// {
//     public class Path : MonoBehaviour
//     {
//         private Transform[] waypoints;
//         private float[] lengths;
//         private float totalLength;
//
//         public Vector3 EndPosition => waypoints[^1].position;
//
//         public Vector3 GetPosition(int index)
//         {
//             return waypoints[index].position;
//         }
//
//         private void Start()
//         {
//             waypoints = new Transform[transform.childCount];
//             for (int i = 0; i < transform.childCount; i++)
//             {
//                 waypoints[i] = transform.GetChild(i);
//             }
//             
//             lengths = new float[waypoints.Length];
//             float sum = 0f;
//             lengths[0] = 0f;
//             for (int i = 1; i < waypoints.Length; i++)
//             {
//                 sum += Vector3.Distance(waypoints[i - 1].position, waypoints[i].position);
//                 lengths[i] = sum;
//             }
//
//             totalLength = sum;
//         }
//
//         /**
//          * t is in [0, 1]. When t=0 return start point, when t=1 return endpoint. 
//          */
//         public Vector3 PositionAt(float distance)
//         {
//             for (int i = 0; i < waypoints.Length; i++)
//             {
//                 if (lengths[i] >= distance)
//                 {
//                     if (i == 0)
//                     {
//                         return waypoints[0].position;
//                     }
//                     else
//                     {
//                         var start = lengths[i - 1];
//                         var stop = lengths[i];
//                         var t = (distance - start) / (stop - start);
//                         return Vector3.Lerp(waypoints[i - 1].position, waypoints[i].position, t);
//                     }
//                 }
//             }
//             return waypoints[^1].position;
//         }
//     }
// }
