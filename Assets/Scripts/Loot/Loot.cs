using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemDetails itemDetailsSO;
    public Animator anim;
    public SpriteRenderer itemIcon;
    public bool canBePickedUp = true;
    public int quantity;
    public static event Action<ItemDetails, int> OnItemLooted;

    private void OnValidate()
    {
        if(itemDetailsSO != null)
        {
            updateAppearance();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && canBePickedUp == true)
        {          
            anim.Play("LootPickUp");
            OnItemLooted?.Invoke(itemDetailsSO, quantity);
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }
    private void updateAppearance()
    {
        itemIcon.sprite = itemDetailsSO.itemIcon;
        this.name = itemDetailsSO.itemName;
    }
    public void initialize(ItemDetails itemDetailsSO, int quantity)
    {
        this.itemDetailsSO = itemDetailsSO;
        this.quantity = quantity;
        canBePickedUp = false;
        updateAppearance();
    }
}
