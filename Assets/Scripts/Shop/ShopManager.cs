using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static event Action<ShopManager, bool > OnShopStateChanged;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private List<ShopItems> shopItemsList;
    [SerializeField] private ShopSlot[] shopSlots;
    private void Start()
    {
        populateShopItems();
        OnShopStateChanged?.Invoke(this, true);
    }
    public void populateShopItems()
    {
        for (int i = 0; i < shopItemsList.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItemsList[i];
            shopSlots[i].Initialize(shopItem.itemDetailsSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }
        for (int i = shopItemsList.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }
    public void tryBuyItem(ItemDetails item, int price)
    {
        if(item != null && inventoryManager.gold >= price)
        {
            if(checkSpaceForItem(item))
            {
                inventoryManager.gold -= price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                inventoryManager.AddItemToInventory(item, 1);
            }
        }
    }
    public bool checkSpaceForItem(ItemDetails item)
    {
        foreach (var slot in inventoryManager.itemSlots)
        {
            if (slot.itemDetailsSO == item && slot.quantity < item.stackValue)
            {
                return true;
            }
            else if (slot.itemDetailsSO == null )
            {
                return true;
            }
        }
        return false;
    }
    public void sellItem(ItemDetails item)
    {
        if(item == null)
        {
            return;
        }
        foreach (var slot in shopSlots)
        {
            if(slot.itemDetailsSO == item)
            {
                inventoryManager.gold += slot.price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }
    [System.Serializable]
    public class ShopItems
    {
        public ItemDetails itemDetailsSO;
        public int price;
    }
}
