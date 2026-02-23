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
    [SerializeField] private Transform groundCheck;

    private PlayerControls controls;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private float currentSpeed;

    private bool jumpQueued;
    private bool isGrounded;
    private bool isPaused;
    private void Awake() {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Ui.SetCallbacks(this);
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }
    private void OnEnable() {
        controls.Ui.Disable();
        controls.Player.Enable();
    }
    private void OnDisable() {
        controls.Disable();
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

    #region Player Inputs
    public void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("Interact Called");
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Jump Called");
            jumpQueued = true;
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
