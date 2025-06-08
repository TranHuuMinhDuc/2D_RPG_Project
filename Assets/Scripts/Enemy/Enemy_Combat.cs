using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;

public class Enemy_Combat : MonoBehaviour
{
    public EnemyDetails EnemyDetailsSO;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && EnemyDetailsSO != null)
        {
            playerHealth.changeHealth(-EnemyDetailsSO.enemyDamage);
        }
    }

}
