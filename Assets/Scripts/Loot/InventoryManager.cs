using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int gold;
    public TMP_Text goldText;
    public UseItem useItem;
    public InventorySlots[] itemSlots;


    private void Start()
    {
        foreach(var slot in itemSlots)
        {
            slot.updateUIIventory();
        }
    }
    #region EventSubscription
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItemToInventory;
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItemToInventory;
    }
    #endregion
    public void AddItemToInventory(ItemDetails itemDetailsSO, int quantity)
    {
        if(itemDetailsSO.isGold)
        {           
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }
        else
        {
            foreach(var slot in itemSlots)
            {
                if(slot.itemDetailsSO == null)
                {
                    slot.itemDetailsSO = itemDetailsSO;
                    slot.quantity = quantity;
                    slot.updateUIIventory();
                    return;
                }
                
            }
        }
       
    }
    public void UseItem(InventorySlots slot)
    {
        if(slot.itemDetailsSO != null && slot.quantity >= 0)
        {
            useItem.applyItemEffect(slot.itemDetailsSO);
            slot.quantity -= 1;
            if(slot.quantity <= 0)
            {
                slot.itemDetailsSO = null;
            }
            slot.updateUIIventory();
        }
    }
}
