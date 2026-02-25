using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,
    PlayerControls.IPlayerActions, 
    PlayerControls.IUiActions {

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 15.0f;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundedCheckDistance;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Events")]
    public System.Action OnInteractPressed;

#region Private Variables
    private PlayerControls controls;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private float currentSpeed;

    private bool isPaused;
#endregion
    private void Awake() {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Ui.SetCallbacks(this);
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }
    private void OnEnable() {
        SwitchToPlayerActionMap();
    }
    private void OnDisable() {
        controls.Disable();
    }
    private void Update() {
        GroundedCheck();
    }
    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        rb.linearVelocity = new Vector2(
            moveInput.x * currentSpeed,
            rb.linearVelocity.y
            );
    }
    private void HandleJump() {
        if (isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void GroundedCheck() {
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        Vector2 boxSize = new Vector2(
            box.bounds.size.x * 0.95f,
            box.bounds.size.y
        );

        RaycastHit2D hit = Physics2D.BoxCast(
            box.bounds.center,
            boxSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }
    private void SwitchToPlayerActionMap() {
        controls.Ui.Disable();
        controls.Player.Enable();
    }
    private void SwitchToUiActionMap() {
        controls.Player.Disable();        
        controls.Ui.Enable();

    }

    private void OnDrawGizmosSelected() {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;

        Vector3 castPosition = box.bounds.center + Vector3.down * groundCheckDistance;

        Gizmos.DrawWireCube(castPosition, box.bounds.size);
    }

    #region Player Inputs
    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Interact Called");
            OnInteractPressed?.Invoke();
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Jump Called");
            HandleJump();
        }
    }
    public void OnLook(InputAction.CallbackContext context) {
    }

    public void OnMouseClick(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Click Called");
        }
    }
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed) {
            currentSpeed = moveSpeed * sprintMultiplier;
        } else if (context.canceled) {
            currentSpeed = moveSpeed;
        }
    }
    #endregion
    #region Ui Inputs
    public void OnNavigate(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }
#endregion
}
