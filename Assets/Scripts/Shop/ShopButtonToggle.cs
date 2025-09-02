using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonToggle : MonoBehaviour
{
    public void openShopItem()
    {
        if(ShopKeeper.currentShopKeeper != null)
            ShopKeeper.currentShopKeeper.openItemShop();
    }
    public void openShopWeapon()
    {
        if(ShopKeeper.currentShopKeeper != null)
            ShopKeeper.currentShopKeeper.openWeaponShop();
    }
    public void openShopArmor()
    {
        if(ShopKeeper.currentShopKeeper != null)
            ShopKeeper.currentShopKeeper.openArmorShop();
    }
}
