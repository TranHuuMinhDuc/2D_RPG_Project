using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Details", menuName = "ScriptableObjects/Loot/Item Details")]
public class ItemDetails : ScriptableObject
{
    [Header("Item Details")]
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;

    public bool isGold;

    [Header("Item Stats")]
    public int maxHealthID;
    public int itemDamageID;
    public int itemSpeedID;

    [Header("Effect")]
    public float itemDuration;
}
