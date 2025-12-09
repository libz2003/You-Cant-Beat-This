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
                    PersistentSettings.instance.playHint = false;
                    PersistentSettings.instance.foundBug = true;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.settingsReaction, true);
                }
                break;
        }
    }

    void SellOption()
    {
        SoundEffectManager.PlayTowerSell();
        PlayerStats.Money += 200000;
        Destroy(gameObject);
    }
}