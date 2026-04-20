using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour,
    PlayerControls.IPlayerActions, 
    PlayerControls.IUiActions {

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 15.0f;

    [Header("Player jump audio")]
    [SerializeField] private PlayerAudio playerAudio;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundedCheckDistance;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animator references")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject spriteObject;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("UI Animation Settings")]
    [SerializeField] private float duration = 3f;
    [SerializeField] private RectTransform dialogueRect;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float moveAmount = 150f;
    [SerializeField] private float animationDuration = 0.4f;

    private Vector2 originalPosition;
    private Coroutine moveCoroutine;

    [Header("Events")]
    public System.Action OnInteractPressed;

    public bool isPaused;

    #region Private Variables
    private PlayerControls controls;

    private Rigidbody2D rb;
     
    private string currentAnimation = "";

    private Vector2 moveInput;

    private float currentSpeed;

    private bool isSprinting;
#endregion
    private void Awake() {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Ui.SetCallbacks(this);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;

        playerAudio = GetComponentInChildren<PlayerAudio>();

        originalPosition = dialogueRect.anchoredPosition;
        canvasGroup.alpha = 0f;

        currentSpeed = moveSpeed;
        //default animation state
        ChangeAnimation("idle");
    }
    private void OnEnable() {
        SwitchToPlayerActionMap();
    }
    private void OnDisable() {
        controls.Disable();
    }
    private void Update() {
        GroundedCheck();
        CheckAnimation();
    }
    private void FixedUpdate() {
        if (isPaused) {
            ChangeAnimation("idle");
            rb.linearVelocity = Vector2.zero;
            return;
        }
        HandleMovement();
    }
    private void CheckAnimation() {
        if (isPaused) {
            return;
        }
        FlipSpriteDependingOnDirection();
        //Idle anims
        //for some reason the player is still moving a little bit when idle, so I added a small threshold to prevent the animation from switching to run
        Vector2 velocity = rb.linearVelocity;
        float speed = Mathf.Abs(velocity.x);

        //Air animations first
        if (!isGrounded) {
            if (rb.linearVelocity.y > 0.1f) {
                ChangeAnimation("jump");
            }
            else if (rb.linearVelocity.y < -0.1f) {
                ChangeAnimation("fall");
            }
            animator.speed = 1f;
            return;
        }

        //Ground animations
        if (speed < 0.2f) {
            animator.speed = 1f;
            ChangeAnimation("idle");
        }
        else {
            animator.speed = isSprinting ? sprintMultiplier : 1f;
            ChangeAnimation("run");
        }
    }
    private void FlipSpriteDependingOnDirection() {
        if (moveInput.x > 0) {
            spriteObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0) {
            spriteObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void ChangeAnimation(string newAnimation,float crossFade = 0.1f) {
        if (currentAnimation == newAnimation) {
            return;
        }
        currentAnimation = newAnimation;
        animator.CrossFade(newAnimation, crossFade);
    }
    private void HandleMovement() {
        rb.linearVelocity = new Vector2(
            moveInput.x * currentSpeed,
            rb.linearVelocity.y
            );
    }
    private void HandleJump() {
        if (isGrounded && !isPaused) {
            playerAudio.PlayJumpSound();
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
    public void ShowDialogueForXTime(string message) {
        dialogueText.text = message;
        playerAudio.PlayTalkSound();
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(AnimateUI(true));
        StartCoroutine(HideDialogueAfterTime(duration));
    }
    private IEnumerator HideDialogueAfterTime(float duration) {
        yield return new WaitForSeconds(duration);
        moveCoroutine = StartCoroutine(AnimateUI(false));
    }
    private IEnumerator AnimateUI(bool fadeIn) {
        float elapsed = 0f;

        Vector2 startPos = dialogueRect.anchoredPosition;
        Vector2 targetPos = fadeIn ? originalPosition + Vector2.up * moveAmount : originalPosition;

        float startAlpha = canvasGroup.alpha;
        float targetAlpha = fadeIn ? 1f : 0f;

        while (elapsed < animationDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            t = Mathf.SmoothStep(0, 1, t);

            dialogueRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            yield return null;
        }

        dialogueRect.anchoredPosition = targetPos;
        canvasGroup.alpha = targetAlpha;
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
            OnInteractPressed?.Invoke();
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            HandleJump();
        }
    }
    public void OnLook(InputAction.CallbackContext context) {
    }

    public void OnMouseClick(InputAction.CallbackContext context) {
        if (context.performed) {
        }
    }
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed) {
            isSprinting = true;
            currentSpeed = moveSpeed * sprintMultiplier;
        } else if (context.canceled) {
            isSprinting = false;
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
