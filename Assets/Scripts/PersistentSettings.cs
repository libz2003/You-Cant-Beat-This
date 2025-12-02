using UnityEngine;

public class PersistentSettings : MonoBehaviour
{
    public static PersistentSettings instance;
    public bool towerObstacle = true;
    public bool optionObstacle = true;
    public bool sellOption = true;

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
