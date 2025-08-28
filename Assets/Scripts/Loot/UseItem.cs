using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void applyItemEffect(ItemDetails item)
    {
        changeItemTypeEffect(item, true);
        if (item.itemDuration > 0)
        {
            StartCoroutine(removeItemEffect(item, item.itemDuration));
        }
    }
    private IEnumerator removeItemEffect(ItemDetails item, float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Removing Effect of " + item.itemName);
        changeItemTypeEffect(item, false);
        checkComsumeItem(false);

    }
    public void changeItemTypeEffect(ItemDetails item, bool isApplying)
    {
        int value;
        if(isApplying)
            value = item.effectValue;
        else 
            value = -item.effectValue;
        switch (item.itemTypeEffect)
        {
            case ItemTypeEffect.MaxHealth:
                StatManager.instance.updateMaxHealth(value, true);
                break;
            case ItemTypeEffect.CurrentHealth:
                if (isApplying)
                    StatManager.instance.updateCurrentHealth(item.effectValue, false);
                break;
            case ItemTypeEffect.Damage:
                StatManager.instance.updateCurrentDamage(value, true);
                break;
            case ItemTypeEffect.Speed:
                StatManager.instance.updateCurrentSpeed(value, true);
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
