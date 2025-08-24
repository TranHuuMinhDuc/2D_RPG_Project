using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int gold;
    public TMP_Text goldText;
    public UseItem useItem;
    public InventorySlots[] itemSlots;
    public GameObject lootPrefab;
    public Transform playerPosition;


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
        //Check if the item is gold
        if (itemDetailsSO.isGold)
        {           
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }
        //Check if the item is stackable and already exists in the inventory
        foreach (var slot in itemSlots)
        {
            if(slot.itemDetailsSO == itemDetailsSO && slot.quantity < itemDetailsSO.stackValue)
            {
                int availableSpace = itemDetailsSO.stackValue - slot.quantity;
                int quantityToAdd = Mathf.Min(availableSpace, quantity);
                slot.quantity += quantityToAdd;
                quantity -= quantityToAdd;
                slot.updateUIIventory();

            }
        }
        //Check for empty slots
        foreach (var slot in itemSlots)
        {
            if(slot.itemDetailsSO == null || slot.quantity <= 0)
            {
                int quantityToAdd = Mathf.Min(itemDetailsSO.stackValue, quantity);
                slot.itemDetailsSO = itemDetailsSO;
                slot.quantity = quantityToAdd;
                quantity -= quantityToAdd;
                slot.updateUIIventory();
                if (quantity <= 0)
                    return;
            }            
        }
       if(quantity > 0)
        {
            dropItemWhenFull(itemDetailsSO, quantity);
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
    public void dropItemWhenFull(ItemDetails itemDetailsSO, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, playerPosition.position, Quaternion.identity).GetComponent<Loot>();
        loot.initialize(itemDetailsSO, quantity);
    }
    public void DropItem(InventorySlots slot)
    {
        dropItemWhenFull(slot.itemDetailsSO, 1);
        slot.quantity--;
        if(slot.quantity <= 0)
        {
            slot.quantity = 0;
            slot.itemDetailsSO = null;
        }
        slot.updateUIIventory();
    }
}
