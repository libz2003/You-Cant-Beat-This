using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private GameObject turretToBuild;
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

    public void SetTurretToBuild(GameObject _turretToBuild)
    {
        turretToBuild = _turretToBuild;
    }

    void Update()
    {
        if (turretToBuild == null)
            return;


        // Detect left-click
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Debug.Log("Building");

        PlaceTurretOnGround();
    }

    void PlaceTurretOnGround()
    {
        // Get mouse position from the new Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0f));

        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0f;

            Instantiate(turretToBuild, hitPoint, Quaternion.identity);
        }

        // built turret, so deselect it
        turretToBuild = null;
    }
}