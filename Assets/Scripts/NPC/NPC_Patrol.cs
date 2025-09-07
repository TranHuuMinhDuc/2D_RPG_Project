using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public float moveSpeed;
    public float pauseDuration;
    public Vector2 target;
    public Vector2[] patrolpoint;

    private Rigidbody2D rb;
    private int currentPatrolIndex;
    private bool isPaused;
    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(setPatrolPoint());
    }
    private void Update()
    {
        if (isPaused) 
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);  
        }
        rb.velocity = direction * moveSpeed;
        if (Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(setPatrolPoint());   
    }
    IEnumerator setPatrolPoint()
    {
        isPaused = true;
        anim.Play("Pawn_Purple_Idle");
        yield return new WaitForSeconds(pauseDuration);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolpoint.Length;
        target = patrolpoint[currentPatrolIndex];
        isPaused = false;
        anim.Play("Pawn_Purple_Walk");
    }
}
