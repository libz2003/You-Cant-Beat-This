using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public GameObject grassPrefab;
        public GameObject pathPrefab;

        public int width = 20;
        public int height = 16;
        public float tileSize = 1.8f;

        // All path tiles in order from start (left) to end (right)
        [SerializeField]
        private List<Vector2Int> pathTiles = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> PathTiles => pathTiles;

        // Control points for a broken line path (x, z) in grid coords
        // You can tweak these to change the shape
        private readonly Vector2Int[] controlPoints = new Vector2Int[]
        {
            new Vector2Int(0, 8),   // enter from left
            new Vector2Int(3, 8),
            new Vector2Int(3, 12),
            new Vector2Int(6, 12),
            new Vector2Int(6, 4),
            new Vector2Int(14, 4),
            new Vector2Int(14, 12),
            new Vector2Int(10, 12),
            new Vector2Int(10, 8),
            new Vector2Int(19, 8)  // exit on right
        };

        private HashSet<Vector2Int> pathSet = new HashSet<Vector2Int>();

        [ContextMenu("Generate Map (Editor)")]
        public void Generate()
        {
            ClearChildren();
            BuildPath();

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2Int gridPos = new Vector2Int(x, z);
                    bool isPath = pathSet.Contains(gridPos);

                    GameObject prefab = isPath ? pathPrefab : grassPrefab;
                    if (prefab == null) continue;

                    Vector3 worldPos = new Vector3(x * tileSize, 0f, z * tileSize);
                    GameObject tile = Instantiate(prefab, worldPos, Quaternion.identity, transform);
                    tile.name = (isPath ? "Path" : "Grass") + "_" + x + "_" + z;
                }
            }
        }
        
        private void BuildPath()
        {
            pathSet.Clear();
            pathTiles.Clear();

            for (int i = 0; i < controlPoints.Length - 1; i++)
            {
                Vector2Int a = controlPoints[i];
                Vector2Int b = controlPoints[i + 1];

                Vector2Int dir = new Vector2Int(
                    b.x == a.x ? 0 : (b.x > a.x ? 1 : -1),
                    b.y == a.y ? 0 : (b.y > a.y ? 1 : -1)
                );

                Vector2Int current = a;
                AddPathTile(current);

                // March in a straight line (horizontal or vertical) to the next control point
                while (current != b)
                {
                    current += dir;
                    AddPathTile(current);
                }
            }
        }
        
        private void AddPathTile(Vector2Int p)
        {
            if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height)
            {
                Debug.LogWarning("Path point outside bounds: " + p);
                return;
            }

            if (pathSet.Add(p))
            {
                pathTiles.Add(p);
            }
        }


        // Remove previously generated tiles so we do not duplicate them
        private void ClearChildren()
        {
            // In editor we have to use DestroyImmediate; in play mode, Destroy
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Destroy(transform.GetChild(i).gameObject);
#endif
            }
        }

        // Optional: still generate automatically when you press Play
        private void Start()
        {
            if (Application.isPlaying)
            {
                Generate();
            }
        }

        // This makes it easy to regenerate from the component’s context menu
        [ContextMenu("Generate Map (Editor)")]
        private void GenerateInEditor()
        {
            Generate();
        }

        // Optional: auto-update whenever you change a value in the inspector
        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Generate();
            }
#endif
        }
    }
}