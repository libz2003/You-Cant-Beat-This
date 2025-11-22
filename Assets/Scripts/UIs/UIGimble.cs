using UnityEngine;

public class UIGimble : MonoBehaviour
{
    void LateUpdate(){
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
