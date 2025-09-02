using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShopManager;

public class ShopKeeper : MonoBehaviour, IInteractable
{
    public ShopManager shopManager;
    public static ShopKeeper currentShopKeeper;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private List<ShopItems> shopItemsList;
    [SerializeField] private List<ShopItems> shopWeaponList;
    [SerializeField] private List<ShopItems> shopArmorList;    
    public static event Action<ShopManager, bool> OnShopStateChanged;   
    private bool isShopOpen;
    public void Interact()
    {
        ToggleShop();
    }
    public void ToggleShop() //TODO: Add inputSystem to control when can open shop
    {
        isShopOpen = !isShopOpen;
        shopPanel.SetActive(isShopOpen);
        currentShopKeeper = this;
        OnShopStateChanged?.Invoke(shopManager, isShopOpen);
        openItemShop();
        if (isShopOpen)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void openItemShop()
    {
        shopManager.populateShopItems(shopItemsList);
    }
    public void openWeaponShop()
    {
        shopManager.populateShopItems(shopWeaponList);
    }
    public void openArmorShop()
    {
        shopManager.populateShopItems(shopArmorList);
    }
    
}
