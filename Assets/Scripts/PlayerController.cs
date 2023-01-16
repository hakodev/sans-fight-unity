using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform sansOverworld;
    private bool isMoving = false;
    private PlayerControls playerControls;
    private Rigidbody2D rb2d;
    private Animator animator;
    
    private Vector2 moveDirection = Vector2.zero;
    public InputAction InputMove { get; set; }
    public InputAction InputInteract { get; set; }
    public InputAction InputCancel { get; set; }

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        SetupMove();
        SetupInteract();
        SetupCancel();
    }

    private void OnDisable() {
        DisableControls();
    }

    private void Update() {
        if (moveDirection != Vector2.zero) {
            isMoving = true;

            if (moveDirection.x != 0 && moveDirection.y != 0) {
                // Keep the player faced the same way when moving diagonally
                float currentDirection = animator.GetFloat("moveX");

                animator.SetFloat("moveX", currentDirection);
            }
            else {
                animator.SetFloat("moveX", moveDirection.x);
                animator.SetFloat("moveY", moveDirection.y);
            }
        }
        else {
            isMoving = false;
        }

        moveDirection = InputMove.ReadValue<Vector2>();
        animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate() {
        var move = new Vector2(moveDirection.x, moveDirection.y);

        if (moveDirection.x != 0 && moveDirection.y != 0) {
            // Keep the player at the same speed as moving straight when moving diagonally
            rb2d.velocity = moveSpeed * 1.4f * move;
        }
        else {
            rb2d.velocity = moveSpeed * move;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "FightTrigger") {
            sansOverworld.position = new Vector2(sansOverworld.position.x, gameObject.transform.position.y);
            Destroy(collision.gameObject);
            // Stop player for Sans dialogue
        }
    }

    private void SetupMove() {
        InputMove = playerControls.Player.Move;
        InputMove.Enable();
        InputMove.performed += Move;
    }

    private void SetupInteract() {
        InputInteract = playerControls.Player.Interact;
        InputInteract.Enable();
        InputInteract.performed += Interact;
    }

    private void SetupCancel() {
        InputCancel = playerControls.Player.Cancel;
        InputCancel.Enable();
        InputCancel.performed += Cancel;
    }

    private void DisableControls() {
        InputMove.Disable();
        InputInteract.Disable();
        InputCancel.Disable();
    }

    private void Move(InputAction.CallbackContext context) { }

    private void Interact(InputAction.CallbackContext context) { }

    private void Cancel(InputAction.CallbackContext context) { }
}
