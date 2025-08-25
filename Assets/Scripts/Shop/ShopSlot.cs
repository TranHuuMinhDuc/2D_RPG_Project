using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("Shop SetUp")]
    public ItemDetails itemDetailsSO;
    public TMP_Text priceText;
    public TMP_Text itemNameText;
    public Image itemIcon;
    public int price;
    [SerializeField] private ShopManager shopManager;
    private void Start()
    {
        shopManager = GetComponentInParent<ShopManager>();
    }
    public void Initialize(ItemDetails item, int price)
    {
        itemDetailsSO = item;
        itemIcon.sprite = itemDetailsSO.itemIcon;
        priceText.text = price.ToString();
        itemNameText.text = itemDetailsSO.itemName;
        this.price = price;
    }
    public void onBuyButtonClicked()
    {
        shopManager.tryBuyItem(itemDetailsSO, price);
    }
}
