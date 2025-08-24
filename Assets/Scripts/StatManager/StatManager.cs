using System.Collections;
using System.Collections.Generic;
using Snorx.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    [Header("Stat Template  ")]
    public PlayerDetails baseStat;

    [Header("Player Details")]
    public string playerNameSM;
    public int currentPlayerLevel;

    [Header("Player Health Details")]
    public int currentPlayerMaxHealth;
    public int currentPlayerHealth;

    [Header("Player Movement Details")]
    public float currentPlayerSpeed;

    [Header("Player Combat Details")]
    public int currentPlayerDamage;
    public float attackCoolDownSM;
    public float knockBackForceSM;
    public float enemyStunTimeSM;
    public float weaponRangeSM;

    [Header("Player State Details")]
    public bool isKnockedBackSM;
    public bool isAttackingSM;
    public bool isConsumeMeat;
    public bool isConsumeFungi;
    public bool isConsumeVegetable;
    public bool isConsumeFruit;

    [Header("Player Components")]
    public Slider healthbar;
    public PlayerHealth playerHealth;
    public StatsUI statsUI;
    
    private void Awake()
    {
        loadBaseStat();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void loadBaseStat()
    {
        playerNameSM = baseStat.playerName;
        currentPlayerLevel = baseStat.playerLevel;
        currentPlayerSpeed = baseStat.playerSpeed;
        currentPlayerMaxHealth = baseStat.playerMaxHealth;
        currentPlayerHealth = baseStat.playerMaxHealth;
        currentPlayerDamage = baseStat.playerDamage;
        attackCoolDownSM = baseStat.attackCoolDown;
        knockBackForceSM = baseStat.knockBackForce;
        enemyStunTimeSM = baseStat.enemyStunTime;
        weaponRangeSM = baseStat.weaponRange;
        isKnockedBackSM = baseStat.isKnockedBack;
        isAttackingSM = baseStat.isAttacking;
    }
    public void updateIncreasedMaxHealth(int amount)
    {
        if (!isConsumeFruit)
        {
            currentPlayerMaxHealth += amount;
            playerHealth.healthSlider.maxValue = StatManager.instance.currentPlayerMaxHealth;
            playerHealth.healthSlider.value = StatManager.instance.currentPlayerHealth;
            isConsumeFruit = true;
        }  
    }
    public void updateCurrentHealth(int amount)
    {
        if(currentPlayerHealth < currentPlayerMaxHealth && !isConsumeVegetable)
        {
            currentPlayerHealth += amount;
            currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, currentPlayerMaxHealth);
            playerHealth.healthSlider.value = currentPlayerHealth;
            isConsumeVegetable = true;
        }
    }
    public void updateCurrentSpeed(int amount)
    {
        if (!isConsumeFungi)
        {
            currentPlayerSpeed += amount;
            statsUI.statsUpdate();
            isConsumeFungi = true;
        }
    }
    public void updateCurrentDamage(int amount)
    {
        if (!isConsumeMeat)
        {
            currentPlayerDamage += amount;
            statsUI.statsUpdate();
            isConsumeMeat = true;
        }     
    }
}
