using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifespan = 3f;
    public float arrowSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * arrowSpeed;
        Destroy(gameObject, lifespan);
    }
}

