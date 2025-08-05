using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Snorx.Data;
using Snorx.Enum;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Camera mainCamera;
    public Animator anim;
    public Player_Combat playerCombat;

    private PlayerInputSystem playerInput;
    private PlayerState playerState;

    #region Parameters
    public Vector2 moveInput { get; private set; }
    public Rigidbody2D rb { get; private set; }  
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInputSystem();
        anim = GetComponent<Animator>();
        changePlayerState(PlayerState.Idle);    
    }

    #region InputSystem
    private void OnEnable()
    {
        playerInput.Player.Move.performed += onMove;
        playerInput.Player.Move.canceled += onMove;
        playerInput.Player.Attack.performed += onAttack;
        playerInput.Player.Attack.canceled += onAttack;
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Move.performed -= onMove;
        playerInput.Player.Move.canceled -= onMove;
        playerInput.Player.Attack.performed -= onAttack;
        playerInput.Player.Attack.canceled -= onAttack;
        playerInput.Disable();
    }
    private void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    #endregion
    private void onAttack(InputAction.CallbackContext context)
    {
        if (!StatManager.instance.isKnockedBackSM && !StatManager.instance.isAttackingSM && playerCombat.attackTimer <= 0)
        {
            StartCoroutine(playerCombat.performAttack());
        }
    }
    private void Update()
    {
        playerFacing();
        if(!StatManager.instance.isKnockedBackSM && !StatManager.instance.isAttackingSM)
        {
            if (playerState == PlayerState.Running && moveInput == Vector2.zero)
            {
                changePlayerState(PlayerState.Idle);
            }
            else if (playerState != PlayerState.Running && moveInput != Vector2.zero)
            {
                changePlayerState(PlayerState.Running);
            }
        }
        if(playerCombat.attackTimer > 0)
        {
            playerCombat.attackTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(!StatManager.instance.isKnockedBackSM && !StatManager.instance.isAttackingSM)
        {
            rb.velocity = moveInput.normalized * StatManager.instance.currentPlayerSpeed;
        }       
    } 

    private void playerFacing()
    {
        if (moveInput.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    #region State Machine
    public void changePlayerState(PlayerState newState)
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);

        playerState = newState;
        switch(playerState)
        {
            case PlayerState.Idle:
                anim.SetBool("isIdle", true);
                rb.velocity = Vector2.zero;
                break;
            case PlayerState.Running:
                anim.SetBool("isMoving", true);
                break;
            case PlayerState.Attacking:
                anim.SetBool("isAttacking", true);
                break;
        }
    }
    #endregion
}
