using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour, IPointerClickHandler
{
    [Header("Item Details")]
    public ItemDetails itemDetailsSO;
    public int quantity;
    [Header("UI Components")]
    public Image itemIcon;
    public TMP_Text quantityText;
    private InventoryManager inventoryManager;
    private static ShopManager activeShop;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }
    private void OnEnable()
    {
        ShopManager.OnShopStateChanged += HandleShopStateChange;
    }
    private void OnDisable()
    {
        ShopManager.OnShopStateChanged -= HandleShopStateChange;
    }
    private void HandleShopStateChange(ShopManager shopManager, bool isOpen)
    {
        if (isOpen)
        {
            activeShop = shopManager;
        }
        else
        {
            activeShop = null;
        }
    }
    public void updateUIIventory()
    {
        if(quantity <= 0)
        {
            itemDetailsSO = null;
            quantity = 0;
        }
        if (itemDetailsSO != null && quantity > 0)
        {
            itemIcon.sprite = itemDetailsSO.itemIcon;
            itemIcon.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemDetailsSO = null;
            quantity = 0;
            itemIcon.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {            
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    activeShop.sellItem(itemDetailsSO);
                    quantity--;
                    updateUIIventory();
                }
                else
                {
                    inventoryManager.UseItem(this);
                }    
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }
}
