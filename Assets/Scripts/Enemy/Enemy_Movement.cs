using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.EnemyData;
using Snorx.Data;
using Snorx.Enum;

public class Enemy_Movement : MonoBehaviour
{
    [Header("Enemy Movement Settings")]
    public EnemyDetails enemyDetailsSO;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private Transform player;    
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyState enemyState;

    private int facingDirection = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        changeState(EnemyState.Idle);
    }
    private void Update()
    {
        if(enemyState != EnemyState.KnockedBack)
        {
            checkForPlayer();
            if (enemyDetailsSO.attackCooldownTimer > 0)
            {
                enemyDetailsSO.attackCooldownTimer -= Time.deltaTime;
            }
            if (enemyState == EnemyState.Chasing)
            {
                chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }         
    }
    private void chase()
    {
        if (enemyState == EnemyState.Chasing)
        {
            if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * enemyDetailsSO.enemySpeed;
        }
    }
    private void flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    #region StateMachine
    public void changeState(EnemyState newState)
    {
        #region Exit current state
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        #endregion
        //Update new enemy state
        enemyState = newState;
        //Enter new state
        anim.SetBool("isIdle", enemyState == EnemyState.Idle);
        anim.SetBool("isChasing", enemyState == EnemyState.Chasing);
        anim.SetBool("isAttacking", enemyState == EnemyState.Attacking);
    }
    #endregion
    #region Check for player 
    private void checkForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position ,enemyDetailsSO.enemyDetectionRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= enemyDetailsSO.attackRange
                && enemyDetailsSO.attackCooldownTimer <= 0)
            {
                enemyDetailsSO.attackCooldownTimer = enemyDetailsSO.attackCooldown;
                changeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > enemyDetailsSO.attackRange)
            {
                changeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            changeState(EnemyState.Idle);
        }           
    }
    #endregion
    private void OnDrawGizmosSelected()
    {   
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, enemyDetailsSO.enemyDetectionRange);
    }
}

