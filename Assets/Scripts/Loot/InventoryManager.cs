using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int gold;
    public TMP_Text goldText;
    public InventorySlots[] itemSlots;

    private void Start()
    {
        foreach(var slot in itemSlots)
        {
            slot.updateUIIventory();
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItemToInventory;
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItemToInventory;
    }
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
}
