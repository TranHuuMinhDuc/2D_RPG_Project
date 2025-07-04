using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;

public class EnemyHealth : MonoBehaviour
{
    public EnemyDetails enemyDetailsSO;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = enemyDetailsSO.enemyMaxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
