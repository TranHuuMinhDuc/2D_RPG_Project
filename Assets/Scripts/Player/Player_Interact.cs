using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    public float interactRange = 1;
    public LayerMask interactableLayer;
    private PlayerInputSystem playerInput;
    public void Awake()
    {
        playerInput = new PlayerInputSystem();
    }

    #region InputSystem
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Interact.performed += onInteract;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Player.Interact.performed -= onInteract;
    }
    public void onInteract(InputAction.CallbackContext context)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);
        if (collider != null)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    #endregion
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}