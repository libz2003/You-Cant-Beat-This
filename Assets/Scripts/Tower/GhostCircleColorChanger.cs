using UnityEngine;

namespace Tower
{
    public class GhostCircleColorChanger : MonoBehaviour
    {
        public GameObject circle;
    
        private Renderer rend;

        private Color yesColor = new Color32(0x11, 0x02, 0x67, 0xC8);
        private Color noColor = new Color32(0xFF, 0x28, 0x28, 0xC8);

        void Awake()
        {
            // Get the Renderer on this object
            rend = circle.GetComponent<Renderer>();
            Debug.Log("Render is not null:" + (rend != null));
        }

        public void SetYesColor()
        {
            rend.material.color = yesColor;
        }

        public void SetNoColor()
        {
            rend.material.color = noColor;
        }
    }
}
