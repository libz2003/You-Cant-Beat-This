using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerSell : MonoBehaviour
{
    [Tooltip("How much this tower originally cost to build.")]
    public int buildCost;

    [Range(0f, 1f)]
    [Tooltip("Percentage of cost refunded when selling.")]
    public float sellRefundPercent = 0.7f;

    void OnMouseOver()
    {
        // Ignore if pointer is over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (Mouse.current == null)
            return;

        // Right-click to sell
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Sell();
        }
    }

    void Sell()
    {
        // Compute refund
        int refund = Mathf.RoundToInt(buildCost * sellRefundPercent);
        if (refund > 0)
        {
            PlayerStats.Money += refund;
        }

        Destroy(gameObject);
    }
}
