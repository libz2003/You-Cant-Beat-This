using Map;
using Tower;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private GameObject turretToBuild;
    private int turretCost;
    private GameObject ghostInstance;
    private GhostCircleColorChanger ghostInstanceColorChanger;
    private Camera mainCamera;

    private Plane groundPlane;   // y = 0 plane

    // public bool canPlaceOnPath;  // use PersistentSettings.canPlaceOnPath
    public float pathWidth;  // unity

    void Awake()
    {
        instance = this;
        mainCamera = Camera.main;

        // Define a plane facing up, positioned at y = 0
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretAndGhost(GameObject _turretToBuild, GameObject _ghostInstance, int _turretCost)
    {
        turretToBuild = _turretToBuild;
        turretCost = _turretCost;
        ghostInstance = _ghostInstance;
        ghostInstance.SetActive(true);
        ghostInstanceColorChanger = ghostInstance.GetComponent<GhostCircleColorChanger>();
    }

    public void UnsetTurretToBuild()
    {
        turretToBuild = null;
        turretCost = 0;
        ghostInstance.SetActive(false);
        ghostInstance = null;
        ghostInstanceColorChanger = null;
    }

    void Update()
    {
        if (turretToBuild == null)
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            ghostInstance.SetActive(false);
            return;
        }

        ghostInstance.SetActive(true);
        Vector3 point = computePlacementPoint();
        point.y = 0f;
        ghostInstance.transform.position = point;
        if (PersistentSettings.instance.canPlaceOnPath || !pointIsOnPath(point, GridWaypointContainer.Waypoints))
        {
            ghostInstanceColorChanger.SetYesColor();
        }
        else
        {
            ghostInstanceColorChanger.SetNoColor();
        }

        // Detect left-click
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // check if we have enough money
        if (PlayerStats.Money >= turretCost) // TODO: change later
        {
            if (PlaceTurretOnGround())
            {
                SoundEffectManager.PlayTowerBuild();
                // PlayerStats.Money -= turretCost;
            }
        }
        else
        {
            UnsetTurretToBuild();
        }
    }

    Vector3 computePlacementPoint()
    {
        // Get mouse position from the new Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0f));

        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0f;

            return hitPoint;
        }

        Debug.LogError("BuildManager.computePlacementPoint(): Unexpected things happened.");
        return default;
    }
    
    public bool pointIsOnPath(Vector3 point, Transform[] waypoints)
    {
        // Treat distance as the full width of the path.
        // If you want "radius" instead, just remove the 0.5f.
        if (waypoints == null || waypoints.Length < 2)
            return false;

        float maxDist = pathWidth * 0.5f;
        float maxDistSq = maxDist * maxDist;

        point.y = 0;  // just compare xz direction

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Vector3 a = waypoints[i].position;
            Vector3 b = waypoints[i + 1].position;

            Vector3 ab = b - a;
            ab.y = 0;
            
            float abLenSq = ab.sqrMagnitude;

            // Degenerate segment: treat as a single point
            if (abLenSq <= Mathf.Epsilon)
            {
                a.y = 0;
                if ((point - a).sqrMagnitude <= maxDistSq)
                {
                    return true;
                }
                continue;
            }

            // Project point onto the segment [a, b]
            float t = Vector3.Dot(point - a, ab) / abLenSq;
            t = Mathf.Clamp01(t);

            Vector3 closest = a + t * ab;
            float distSq = (point - closest).sqrMagnitude;

            if (distSq <= maxDistSq)
            {
                return true;
            }
        }

        return false;
    }

    /**
     * Returns if placement is successful
     */
    bool PlaceTurretOnGround()
    {
        Vector3 hitPoint = computePlacementPoint();
        
        if (PersistentSettings.instance.canPlaceOnPath || !pointIsOnPath(hitPoint, GridWaypointContainer.Waypoints))
        {
            GameObject turretInstance = Instantiate(turretToBuild, hitPoint, Quaternion.identity);

            // If the placed tower is sellable, record how much it cost
            TowerSell sellComp = turretInstance.GetComponent<TowerSell>();
            if (sellComp != null)
            {
                sellComp.buildCost = turretCost;
            }

            PlayerStats.Money -= turretCost;
            UnsetTurretToBuild();
            return true;
        }
        else
        {
            UnsetTurretToBuild();
            return false;
        }

    }
}