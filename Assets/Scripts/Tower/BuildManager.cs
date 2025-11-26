using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private GameObject turretToBuild;
    private int turretCost;
    private GameObject ghostInstance;
    private Camera mainCamera;

    private Plane groundPlane;   // y = 0 plane

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
    }

    public void UnsetTurretToBuild()
    {
        turretToBuild = null;
        turretCost = 0;
        ghostInstance.SetActive(false);
        ghostInstance = null;
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
        point.y = 1f;
        ghostInstance.transform.position = point;

        // Detect left-click
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // check if we have enough money
        if (PlayerStats.Money >= turretCost) // TODO: change later
        {
            PlayerStats.Money -= turretCost;
            PlaceTurretOnGround();
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

    void PlaceTurretOnGround()
    {
        Vector3 hitPoint = computePlacementPoint();
        Instantiate(turretToBuild, hitPoint, Quaternion.identity);

        // built turret, so deselect it and hide ghost
        // turretToBuild = null;
        // ghostInstance.SetActive(false);
        UnsetTurretToBuild();
    }
}