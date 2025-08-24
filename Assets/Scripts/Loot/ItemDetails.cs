using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Details", menuName = "ScriptableObjects/Loot/Item Details")]
public class ItemDetails : ScriptableObject
{
    [Header("Item Details")]
    public ItemTypeEffect itemTypeEffect;
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;
    public bool isGold;

    [Header("Effect")]
    public int effectValue;
    public float itemDuration;
}
