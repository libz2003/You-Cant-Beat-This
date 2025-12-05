using UnityEngine;
using UnityEngine.UI;

public class WinScreenBadges : MonoBehaviour
{
    [Header("Icons")]
    public Image canPlaceOnPathImage;
    public Image optionObstacleImage;
    public Image sellOptionImage;
    public Image bankBreakableImage;
    public Image treeCuttableImage;

    [Header("Colors")]
    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.white;

    void OnEnable()
    {
        UpdateIcons();
    }

    void UpdateIcons()
    {
        if (PersistentSettings.instance == null)
        {
            Debug.LogWarning("WinScreenBadges: PersistentSettings.instance is null.");
            return;
        }

        // Based on your numberBugRemaining logic:
        // unlocked when the corresponding target flag is false
        SetIcon(canPlaceOnPathImage, !PersistentSettings.instance.targetCanPlaceOnPath);
        SetIcon(optionObstacleImage, !PersistentSettings.instance.targetOptionObstacle);
        SetIcon(sellOptionImage, !PersistentSettings.instance.targetSellOption);
        SetIcon(bankBreakableImage, !PersistentSettings.instance.targetBankBreakable);
        SetIcon(treeCuttableImage, !PersistentSettings.instance.targetTreeCuttable);
    }

    void SetIcon(Image img, bool unlocked)
    {
        if (img == null) return;
        img.color = unlocked ? unlockedColor : lockedColor;
    }
}
