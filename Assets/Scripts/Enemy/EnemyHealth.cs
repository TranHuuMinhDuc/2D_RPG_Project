        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;

public class EnemyHealth : MonoBehaviour
{
    public EnemyDetails enemyDetailsSO;
    public int currentHealth;
    public delegate void enemyDeathEXP(int exp);
    public static event enemyDeathEXP onEnemyDeathEXP;

    private void Awake()
    {
        currentHealth = enemyDetailsSO.enemyMaxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > enemyDetailsSO.enemyMaxHealth)
        {
            currentHealth = enemyDetailsSO.enemyMaxHealth;
        }
        else if(currentHealth <= 0)
        {
            onEnemyDeathEXP(enemyDetailsSO.enemyExpDrop);
            Destroy(gameObject);
        }
    }
}
