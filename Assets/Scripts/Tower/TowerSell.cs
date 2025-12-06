using UnityEngine;

public class TowerSell : MonoBehaviour
{
    [Tooltip("How much this tower originally cost to build.")]
    public int buildCost;

    [Range(0f, 1f)]
    [Tooltip("Percentage of cost refunded when selling.")]
    public float sellRefundPercent = 0.7f;

    public void SellTower()
    {
        int refund = Mathf.RoundToInt(buildCost * sellRefundPercent);
        if (refund > 0)
        {
            PlayerStats.Money += refund;
        }
        SoundEffectManager.PlayTowerSell();

        // TODO: play some audio / particle FX here if you want

        Destroy(gameObject);
    }
}
