using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public PlayerInputSystem playerInput;
    public Player_Combat playerCombat;
    private Vector2 aimDirection = Vector2.right;
    private void Awake()
    {
        playerInput = new PlayerInputSystem();
    }
    #region InputSystem
    private void OnEnable()
    {
        playerInput.Player.Shoot.performed += OnShoot;
        playerInput.Player.Shoot.canceled += OnShoot;
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Shoot.performed -= OnShoot;
        playerInput.Player.Shoot.canceled -= OnShoot;
        playerInput.Disable();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!StatManager.instance.isKnockedBackSM && !StatManager.instance.isAttackingSM 
            && playerCombat.attackTimer <= 0)
        {
            shootArrow();
        }
    }
    #endregion
    public void shootArrow()
    {
        Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);
    }
    public void HandleAiming()
    {
       
    }
}
