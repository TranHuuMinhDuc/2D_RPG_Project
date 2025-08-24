using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void applyItemEffect(ItemDetails item)
    {
        changeItemType(item, true);
        if (item.itemDuration > 0)
        {
            StartCoroutine(removeItemEffect(item, item.itemDuration));
        }
    }
    private IEnumerator removeItemEffect(ItemDetails itemDetailsSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        changeItemType(itemDetailsSO, false);
        checkComsumeItem(false);

    }
    public void changeItemType(ItemDetails item, bool isApplying)
    {
        int value;
        if(isApplying)
        {
            value = item.effectValue;
        }
        else
        {
            value = -item.effectValue;
        }
        switch (item.itemTypeEffect)
        {
            case ItemTypeEffect.MaxHealth:
                StatManager.instance.updateIncreasedMaxHealth(value);
                break;
            case ItemTypeEffect.CurrentHealth:
                StatManager.instance.updateCurrentHealth(value);
                break;
            case ItemTypeEffect.Damage:
                StatManager.instance.updateCurrentDamage(value);
                break;
            case ItemTypeEffect.Speed:
                StatManager.instance.updateCurrentSpeed(value);
                break;
        }
    }
    public void checkComsumeItem(bool isComsume)
    {
        StatManager.instance.isConsumeMeat = isComsume;
        StatManager.instance.isConsumeFungi = isComsume;
        StatManager.instance.isConsumeVegetable = isComsume;
        StatManager.instance.isConsumeFruit = isComsume;
    }
}
