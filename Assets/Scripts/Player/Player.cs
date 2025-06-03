using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Snorx.Data;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public PlayerDetails playerDetailsSO;
    [SerializeField] private Camera mainCamera;
    public Animator animator;

    private Rigidbody2D rb;
    private PlayerInputSystem playerInput;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInputSystem();
        animator = GetComponent<Animator>();
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
        HandleAnimation();
        playerFacing();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * playerDetailsSO.playerSpeed;
    }

    private void HandleAnimation()
    {
        animator.SetBool("isMoving", moveInput != Vector2.zero);
        animator.SetBool("isIdle", moveInput == Vector2.zero);
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
}
