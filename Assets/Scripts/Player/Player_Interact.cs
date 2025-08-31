using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    public PlayerInputSystem playerInput;
    public LayerMask interactableLayer;

    private bool isPlayerInRange;
    #region InputSystem
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    #endregion
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
