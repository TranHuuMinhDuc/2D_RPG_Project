using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] wallCollider;
    public Collider2D[] boundaryCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D wall in wallCollider)
            {
                wall.enabled = true;
            }
            foreach (Collider2D boundary in boundaryCollider)
            {
                boundary.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }

    }
}
