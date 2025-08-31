using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        ShopManager.instance.ToggleShop();
    }
}
