using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SellManager : MonoBehaviour
{
    public Camera mainCamera;

    void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {

        // Do not sell if mouse is over UI
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        if (Mouse.current == null)
            return;

        // Only on right click
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(
            new Vector3(mousePos.x, mousePos.y, 0f)
        );

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Find a TowerSell on the hit object or its parents
            TowerSell sellComp = hit.collider.GetComponentInParent<TowerSell>();
            if (sellComp != null)
            {
                sellComp.SellTower();

                // Match your SettingSell behavior
                PersistentSettings.instance.targetSellOption = false;
            }
        }
    }
}
