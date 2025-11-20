using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPlacer : MonoBehaviour
{
    [Header("Required")]
    public GameObject prefabToPlace;
    public GameObject ghostInstance;

    [Header("Other settings")]
    public float fixedY = 6f;   // always place at this height
    public float maxDistance = 1000f;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        ghostInstance.SetActive(false);
    }

    void Update()
    {
        if (mainCam == null || ghostInstance == null)
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            ghostInstance.SetActive(false);
            return;
        }

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        // Intersect with horizontal plane y = fixedY
        if (Mathf.Abs(ray.direction.y) < 0.0001f)
        {
            // Ray is almost parallel to the plane, skip
            ghostInstance.SetActive(false);
            return;
        }

        float t = (fixedY - ray.origin.y) / ray.direction.y;

        if (t <= 0f || t > maxDistance)
        {
            ghostInstance.SetActive(false);
            return;
        }

        Vector3 placePosition = ray.origin + t * ray.direction;

        // Optional: snap to grid
        // placePosition.x = Mathf.Round(placePosition.x);
        // placePosition.z = Mathf.Round(placePosition.z);

        ghostInstance.transform.position = placePosition;
        ghostInstance.transform.rotation = Quaternion.identity; // or whatever you want
        ghostInstance.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject(placePosition, ghostInstance.transform.rotation);
        }
    }

    void PlaceObject(Vector3 position, Quaternion rotation)
    {
        if (prefabToPlace == null)
            return;

        Instantiate(prefabToPlace, position, rotation);
    }
}