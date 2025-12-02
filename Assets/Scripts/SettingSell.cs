using UnityEngine;
using UnityEngine.EventSystems;

public class SettingSell : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
                if (PersistentSettings.instance.sellOption)
                {   
                    // TODO: audio
                    SellOption();
                    PersistentSettings.instance.targetSellOption = false;
                }
                break;
        }
    }

    void SellOption()
    {
        PlayerStats.Money += 200000;
        Destroy(gameObject);
    }
}