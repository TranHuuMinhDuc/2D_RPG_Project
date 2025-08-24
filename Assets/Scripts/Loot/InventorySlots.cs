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

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    public void updateUIIventory()
    {
        if (itemDetailsSO != null)
        {
            itemIcon.sprite = itemDetailsSO.itemIcon;
            itemIcon.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }
        }
    }

}
