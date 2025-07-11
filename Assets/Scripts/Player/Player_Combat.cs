using System.Collections;
using System.Collections.Generic;
using Snorx.Data;
using Snorx.Enum;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Player playerSO;
    public PlayerDetails playerDetailsSO;
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;

    private PlayerState playerState;

    public bool isKnockedBack { get; private set; }
    public bool isAttacking { get; private set; }
    public float attackTimer;

    #region IEnumerators
    public IEnumerator knockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(1);
        playerSO.rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
    public IEnumerator performAttack()
    {
        isAttacking = true; 
        if (attackTimer <= 0)
        {
            playerSO.changePlayerState(PlayerState.Attacking);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-(playerDetailsSO.playerDamage));
                enemies[0].GetComponent<Enemy_KnockBack>().enemyKnockedBack(transform, playerDetailsSO.knockBackForce, playerDetailsSO.enemyStunTime);
            }
            attackTimer = playerDetailsSO.attackCoolDown;
        }
        playerSO.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        if (playerSO.moveInput == Vector2.zero)
        {
            playerSO.changePlayerState(PlayerState.Idle);   
        }
        else
        {
            playerSO.changePlayerState(PlayerState.Running);
        }
    }
    #endregion
    public void EndAttackAnimation()
    {
        if (playerState == PlayerState.Attacking)
        {
            playerSO.anim.SetBool("isAttacking", false);
        }
    }
    public void playerKnockedBack(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        playerSO.rb.velocity = direction * force;
        StartCoroutine(knockBackCounter(stunTime));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
