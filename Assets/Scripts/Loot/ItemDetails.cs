using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Details", menuName = "ScriptableObjects/Loot/Item Details")]
public class ItemDetails : ScriptableObject
{
    [Header("Item Category")]
    public ItemType itemType;
    public ItemTypeEffect itemTypeEffect;
    [Header("Item Details")]
    public string itemName;
    [TextArea] public string itemDescription;
    [Header("UI Components")]
    public Sprite itemIcon;
    [Header("Item Value")]
    public int stackValue;
    [Header("Effect")]
    public int effectValue;
    public float itemDuration;
    [Header("Check Item State")]
    public bool isGold;
}
