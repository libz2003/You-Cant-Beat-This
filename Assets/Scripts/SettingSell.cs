using UnityEngine;
using UnityEngine.EventSystems;

public class SettingSell : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
                SellOption();
                break;
        }
    }

    void SellOption()
    {
        PlayerStats.Money += 200000;
        Destroy(gameObject);
    }
}