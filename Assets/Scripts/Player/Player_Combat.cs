using System.Collections;
using System.Collections.Generic;
using Snorx.Data;
using Snorx.Enum;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Player playerSO;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float testWeaponRange = 1;
    public StatsUI statsUI;
    public float attackTimer;

    private PlayerState playerState;

    #region IEnumerators
    public IEnumerator knockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(1);
        playerSO.rb.velocity = Vector2.zero;
        StatManager.instance.isKnockedBackSM = false;
    }
    public IEnumerator performAttack()
    {
        StatManager.instance.isAttackingSM = true; 
        if (attackTimer <= 0)
        {
            playerSO.changePlayerState(PlayerState.Attacking);
            attackTimer = StatManager.instance.attackCoolDownSM;
        }
        playerSO.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        StatManager.instance.isAttackingSM = false;
        if (playerSO.moveInput == Vector2.zero)
        {
            playerSO.changePlayerState(PlayerState.Idle);   
        }
        else
        {
            playerSO.changePlayerState(PlayerState.Running);
        }
    }
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatManager.instance.weaponRangeSM, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-(StatManager.instance.currentPlayerDamage));
            enemies[0].GetComponent<Enemy_KnockBack>().enemyKnockedBack(transform,
                StatManager.instance.knockBackForceSM, StatManager.instance.enemyStunTimeSM);
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
        StatManager.instance.isKnockedBackSM = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        playerSO.rb.velocity = direction * force;
        StartCoroutine(knockBackCounter(stunTime));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, testWeaponRange);
    }
}
