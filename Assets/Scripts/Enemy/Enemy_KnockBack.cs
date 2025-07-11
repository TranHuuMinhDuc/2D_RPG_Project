using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;

public class Enemy_KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_Movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }
    public void enemyKnockedBack(Transform playerTransform, float knockBackForce, float stunTime)
    {
        enemy_Movement.changeState(EnemyState.KnockedBack);
        StartCoroutine(stunTimer(stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockBackForce;
    }
    IEnumerator stunTimer(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        enemy_Movement.changeState(EnemyState.Idle);
    }
}
