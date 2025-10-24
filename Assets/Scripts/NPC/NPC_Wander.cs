using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderY;
    public float wanderX;
    [Header("Wander Settings")]
    public float moveSpeed;
    public float wanderDuration;
    public Vector2 startingPosition;
    public Vector2 target;

    private Rigidbody2D rb;
    private bool isPaused;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        startingPosition = transform.position;
    }
    private void Update()
    {
        if (isPaused)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Pawn_Purple_Idle");
            return;
        }
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(pauseWander());
        }
        move();
    }
    private void OnEnable()
    {
        StartCoroutine(pauseWander());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        anim.Play("Pawn_Purple_Idle");
    }
    IEnumerator pauseWander()
    {
        isPaused = true;
        anim.Play("Pawn_Purple_Idle");
        yield return new WaitForSeconds(wanderDuration);
        target = getRandomTarget();
        isPaused = false;
        anim.Play("Pawn_Purple_Walk");
    }
    private void move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rb.velocity = direction * moveSpeed;
    }
    private Vector2 getRandomTarget()
    {
        float halfX = wanderX / 2;
        float halfY = wanderY / 2;
        int edge = Random.Range(0, 4);
        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfX, Random.Range(startingPosition.y - halfY, startingPosition.y + halfY)),
            1 => new Vector2(startingPosition.x + halfX, Random.Range(startingPosition.y - halfY, startingPosition.y + halfY)),
            2 => new Vector2(Random.Range(startingPosition.y - halfX, startingPosition.y + halfX), startingPosition.y - halfY),
            _ => new Vector2(Random.Range(startingPosition.y - halfX, startingPosition.y + halfX), startingPosition.y + halfY),


        };
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(pauseWander());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderX, wanderY, 0));
    }
}
