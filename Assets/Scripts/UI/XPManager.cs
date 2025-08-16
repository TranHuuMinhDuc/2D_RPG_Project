using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class XPManager : MonoBehaviour
{
    public int currentXP;
    public int expForNextLevel = 10;
    public float expMultiplier = 1.2f;
    public Slider expSlide;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        updateExpUI();
    }
    private void OnEnable()
    {
        EnemyHealth.onEnemyDeathEXP += gainExp;
    }
    private void OnDisable()
    {
        EnemyHealth.onEnemyDeathEXP -= gainExp;
    }
    public void gainExp(int amount)
    {
        currentXP += amount;
        if (currentXP >= expForNextLevel)
        {
            levelUp();
            expSlide.maxValue = expForNextLevel;
        }
        updateExpUI();
    }
    public void levelUp()
    {
        currentXP -= expForNextLevel;
        StatManager.instance.currentPlayerLevel++;
        expForNextLevel = Mathf.RoundToInt(expForNextLevel * expMultiplier);
        OnLevelUp?.Invoke(2);
    }
    public void updateExpUI()
    {
        expSlide.value = expForNextLevel;
        expSlide.value = currentXP;
        currentLevelText.text = "Level: " + StatManager.instance.currentPlayerLevel;
    }
}
