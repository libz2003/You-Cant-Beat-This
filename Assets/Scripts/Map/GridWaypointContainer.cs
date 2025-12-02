using System;
using UnityEngine;

namespace Map
{
    public class GridWaypointContainer : MonoBehaviour
    {
        public static GridWaypointContainer Instance;

        public static Transform[] Waypoints;

        public Transform[] waypointsCopy;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Waypoints = waypointsCopy;
        }
    }
}
