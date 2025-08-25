using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItems> shopItemsList;
    [SerializeField] private ShopSlot[] shopSlots;
    private void Start()
    {
        populateShopItems();
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
    [System.Serializable]
    public class ShopItems
    {
        public ItemDetails itemDetailsSO;
        public int price;
    }
}
