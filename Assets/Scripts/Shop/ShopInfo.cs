using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShopInfo : MonoBehaviour
{
    [Header("UI Component")]
    public GameObject infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    [Header("Stat Field")]
    public TMP_Text[] statText;
    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }
    public void showItemInfo(ItemDetails item)
    {
        infoPanel.SetActive(true);
        itemNameText.text = item.name;
        itemDescriptionText.text = item.itemDescription;
        checkShopItemStat(item);
        
    }
    public void hideItemInfo()
    {
        infoPanel.SetActive(false);
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }
    public void followMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);
        infoPanelRect.position = mousePos + offset;
    }
    public void checkShopItemStat(ItemDetails item)
    {
        List<string> stats = new List<string>();
        if (item.itemTypeEffect == ItemTypeEffect.MaxHealth && item.effectValue > 0)
            stats.Add("MaxHP: " + item.effectValue.ToString());
        if (item.itemTypeEffect == ItemTypeEffect.CurrentHealth && item.effectValue > 0)
            stats.Add("HP: " + item.effectValue.ToString());
        if (item.itemTypeEffect == ItemTypeEffect.Damage && item.effectValue > 0)
            stats.Add("Damge: " + item.effectValue.ToString());
        if (item.itemTypeEffect == ItemTypeEffect.Speed && item.effectValue > 0)
            stats.Add("Speed: " + item.effectValue.ToString());
        if (item.itemDuration > 0)
            stats.Add("Duration: " + item.itemDuration.ToString());
        if (stats.Count <= 0) return;
        for (int i = 0; i < statText.Length; i++)
        {
            if(i < stats.Count)
            {
                statText[i].text = stats[i];
                statText[i].gameObject.SetActive(true);
            }
            else
            {
                statText[i].gameObject.SetActive(false);
            }
            
        }
    }
}
 