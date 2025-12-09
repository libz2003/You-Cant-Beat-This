using UnityEngine;

public class PersistentSettings : MonoBehaviour
{
    public static PersistentSettings instance;
    public bool canPlaceOnPath = true;
    public bool targetCanPlaceOnPath = true; // Tower/Turret
    public bool optionObstacle = true;
    public bool targetOptionObstacle = true; // UIs/OptionColliderController
    public bool sellOption = true;
    public bool targetSellOption = true; // SettingSell
    public bool bankBreakable = true;
    public bool targetBankBreakable = true; // BankHit
    public bool treeCuttable = true;
    public bool targetTreeCuttable = true; // Tree/TreeCollisionManager AND Tree/TreeTipCollisionManager
    public bool playHint = false;
    public bool playBugFixed = false;
    public bool foundBug = false;
    public int playThroughCount = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int numberBugRemaining()
    {   
        int remaining = 5;
        if (!targetCanPlaceOnPath)
            remaining -= 1;
        if (!targetOptionObstacle)
            remaining -= 1;
        if (!targetSellOption)
            remaining -= 1;
        if (!targetBankBreakable)
            remaining -= 1;
        if (!targetTreeCuttable)
            remaining -= 1;

        return remaining;
    }
}
