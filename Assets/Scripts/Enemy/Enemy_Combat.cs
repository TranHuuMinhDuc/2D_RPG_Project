using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;

public class Enemy_Combat : MonoBehaviour
{
    public EnemyDetails EnemyDetailsSO;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, EnemyDetailsSO.weaponRange, playerLayer);
        if(hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().changeHealth(-EnemyDetailsSO.enemyDamage);
            hits[0].GetComponent<Player>().knockBack(transform, EnemyDetailsSO.knockBackForce, EnemyDetailsSO.stunTime);
        }
    }
}
