using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [Header("Shop SetUp")]
    public ItemDetails itemDetailsSO;
    public TMP_Text priceText;
    public TMP_Text itemNameText;
    public Image itemIcon;
    public int price;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopInfo shopInfo;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(itemDetailsSO != null)
            shopInfo.showItemInfo(itemDetailsSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {  
            shopInfo.hideItemInfo();      
    }

    public void OnPointerMove(PointerEventData eventData)
    {       
            shopInfo.followMouse();
    }

}
