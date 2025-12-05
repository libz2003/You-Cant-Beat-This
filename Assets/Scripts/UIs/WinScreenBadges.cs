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

    public Color unlockedColor = new Color(255, 255, 255, 200);

    void OnEnable()
    {
        // UpdateIcons();

        // TestAllOn();
        TestAllOff();
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

    void TestAllOn()
    {
        SetIcon(canPlaceOnPathImage, true);
        SetIcon(optionObstacleImage, true);
        SetIcon(sellOptionImage, true);
        SetIcon(bankBreakableImage, true);
        SetIcon(treeCuttableImage, true);
    }
    
    void TestAllOff()
    {
        SetIcon(canPlaceOnPathImage, false);
        SetIcon(optionObstacleImage, false);
        SetIcon(sellOptionImage, false);
        SetIcon(bankBreakableImage, false);
        SetIcon(treeCuttableImage, false);
    }

    void SetIcon(Image img, bool unlocked)
    {
        if (img == null) return;
        img.color = unlocked ? unlockedColor : lockedColor;
    }
}
