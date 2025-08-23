using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    [Header("Item Details")]
    public ItemDetails itemDetailsSO;
    public int quantity;
    [Header("UI Components")]
    public Image itemIcon;
    public TMP_Text quantityText;

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
}
