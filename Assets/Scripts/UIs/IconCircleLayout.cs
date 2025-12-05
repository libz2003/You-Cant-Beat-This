using UnityEngine;

public class IconCircleLayout : MonoBehaviour
{
    public RectTransform[] icons;
    public float radius = 200f;
    public float startAngleDegrees = 90f;

    void OnEnable()
    {
        LayoutIcons();
    }

    void OnValidate()
    {
        // So it updates in the editor when you tweak radius/angles
        LayoutIcons();
    }

    void LayoutIcons()
    {
        if (icons == null || icons.Length == 0)
            return;

        float step = 360f / icons.Length;

        for (int i = 0; i < icons.Length; i++)
        {
            RectTransform rt = icons[i];
            if (rt == null) continue;

            float angleDeg = startAngleDegrees + step * i;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float x = Mathf.Cos(angleRad) * radius;
            float y = Mathf.Sin(angleRad) * radius;

            rt.anchoredPosition = new Vector2(x, y);
        }
    }
}
