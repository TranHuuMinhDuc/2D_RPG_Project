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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInputSystem();
        anim = GetComponent<Animator>();
    }

    #region InputSystem
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Move.performed += onMove;
        playerInput.Player.Move.canceled += onMove;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Move.performed -= onMove;
        playerInput.Player.Move.canceled -= onMove;
    }
    private void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    #endregion

    private void Update()
    {
        playerFacing();
        if(isKnockedBack == false)
        {
            if (playerState == PlayerState.Running && moveInput == Vector2.zero)
            {
                changePlayerState(PlayerState.Idle);
            }
            else if (moveInput != Vector2.zero && playerState != PlayerState.Running)
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

    private void changePlayerState(PlayerState newState)
    {
        if (playerState == PlayerState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (playerState == PlayerState.Running)
        {
            anim.SetBool("isMoving", false);
        }
        
        playerState = newState;

        anim.SetBool("isIdle", moveInput == Vector2.zero);
        anim.SetBool("isMoving", moveInput != Vector2.zero);
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
}
