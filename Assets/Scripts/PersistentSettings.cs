using UnityEngine;

public class PersistentSettings : MonoBehaviour
{
    public static PersistentSettings instance;
    public bool canPlaceOnPath = true;
    public bool targetCanPlaceOnPath = true; // Turret
    public bool optionObstacle = true;
    public bool targetOptionObstacle = true;
    public bool sellOption = true;
    public bool targetSellOption = true;

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
}
