using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Snorx.Data;
using Snorx.Enum;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public PlayerDetails playerDetailsSO;
    [SerializeField] private Camera mainCamera;
    public Animator anim;

    private Rigidbody2D rb;
    private PlayerInputSystem playerInput;
    private Vector2 moveInput;
    private PlayerState playerState;
    private bool isKnockedBack;
    private bool isAttacking;
    public Player_Combat playerCombat;

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
        playerInput.Enable();
        playerInput.Player.Move.performed += onMove;
        playerInput.Player.Move.canceled += onMove;
        playerInput.Player.Attack.performed += onAttack;
        playerInput.Player.Attack.canceled += onAttack;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Move.performed -= onMove;
        playerInput.Player.Move.canceled -= onMove;
        playerInput.Player.Attack.performed -= onAttack;
        playerInput.Player.Attack.canceled -= onAttack;
    }
    private void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    #endregion
    private void onAttack(InputAction.CallbackContext context)
    {
        if (!isKnockedBack)
        {
            StartCoroutine(performAttack());
        }
    }
    private void Update()
    {
        playerFacing();
        if(isKnockedBack == false)
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
        
    }

    private void FixedUpdate()
    {
        if(isKnockedBack == false)
        {
            rb.velocity = moveInput.normalized * playerDetailsSO.playerSpeed;
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
                StartCoroutine(EndAttackAnimation());
                break;
        }
    }

    public void knockBack(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(knockBackCounter(stunTime));
    }

    IEnumerator knockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
    IEnumerator performAttack( )
    {
        changePlayerState(PlayerState.Attacking);
        yield return new WaitForSeconds(0.5f);
        if (moveInput == Vector2.zero)
        {
            changePlayerState(PlayerState.Idle);
        }
        else
        {
            changePlayerState(PlayerState.Running);
        }
    }
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(1);
        if (playerState == PlayerState.Attacking)
        {
            changePlayerState(moveInput == Vector2.zero ? PlayerState.Idle : PlayerState.Running);
        }
    }

}
