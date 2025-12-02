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
                    SellOption();
                    PersistentSettings.instance.targetSellOption = false;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.settingsReaction);
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