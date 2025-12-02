using UnityEngine;

namespace Tower
{
    public class GhostCircleColorChanger : MonoBehaviour
    {
        public GameObject circle;
    
        private Renderer rend;

        private Color yesColor = new Color32(0x11, 0x02, 0x67, 0xFF);
        private Color noColor = new Color32(0xff, 0xff, 0xff, 0xff);

        void Awake()
        {
            // Get the Renderer on this object
            rend = circle.GetComponent<Renderer>();
            Debug.Log("Render is not null:" + (rend != null));
        }

        public void SetYesColor()
        {
            Debug.Log("yes");
            rend.material.color = yesColor;
        }

        public void SetNoColor()
        {
            Debug.Log("no");
            rend.material.color = noColor;
        }
    }
}
