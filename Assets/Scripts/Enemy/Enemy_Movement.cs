using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;
using Snorx.Data;

public class Enemy_Movement : MonoBehaviour
{
    [Header("Enemy Movement Settings")]
    public EnemyDetails enemyDetailsSO;
    public Transform player;    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < enemyDetailsSO.chaseRange) 
        {
            enemyDetailsSO.isChasing = true;
            enemyDetailsSO.timeOutOfRange = 0;
            if (!enemyDetailsSO.isTouchingPlayer)
            {
                chasePlayer();
            }
        }
        else
        {
            if (enemyDetailsSO.isChasing)
            {
                enemyDetailsSO.timeOutOfRange += Time.deltaTime;
                if (enemyDetailsSO.timeOutOfRange >= enemyDetailsSO.stopChaseTime)
                {
                    enemyDetailsSO.isChasing = false;
                }
                else
                {
                    if (!enemyDetailsSO.isTouchingPlayer)
                    {
                        chasePlayer();
                    }
                }
            }
        }
    }
    private void chasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * enemyDetailsSO.enemySpeed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetailsSO.chaseRange);
    }

    #region HandleCollisionEnter&Exit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyDetailsSO.isTouchingPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyDetailsSO.isTouchingPlayer = false;
        }
    }
    #endregion
}
