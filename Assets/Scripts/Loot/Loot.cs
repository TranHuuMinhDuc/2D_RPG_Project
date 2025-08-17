using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemDetails itemDetailsSO;
    public Animator anim;
    public SpriteRenderer itemIcon;

    public int quantity;

    private void OnValidate()
    {
        if(itemDetailsSO != null)
        {
            itemIcon.sprite = itemDetailsSO.itemIcon;
            this.name = itemDetailsSO.itemName;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {          
            anim.Play("LootPickUp");
            Destroy(gameObject, 0.5f);
        }
    }
}
