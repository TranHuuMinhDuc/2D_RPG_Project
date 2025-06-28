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
        if(enemyState == EnemyState.Chasing)
        {
            if (player.position.x > transform.position.x && facingDirection == -1||
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
    private void changeState(EnemyState newState)
    {
        //Exit current state
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        //Update new enemy state
        enemyState = newState;
        //Enter new state
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
    }
    #region OnTrigger Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(player == null)
            {
                player = collision.transform;
            }
            changeState(EnemyState.Chasing);
        }      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;;
            changeState(EnemyState.Idle);
        }
    }
    #endregion
    
}

