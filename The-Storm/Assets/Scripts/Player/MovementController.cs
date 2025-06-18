using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class MovementController : NetworkBehaviour
{
    [Header("Component References")]
    private ServerUpdates serverUpdates;
    private CharacterController characterController;
    private Controls controls;
    private InputAction move;
    private InputAction jump;

    [Header("Environment")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private bool jumpQueued = false;

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Vector3 velocity;
    private Vector3 lastGroundedMovementDirection;

    [Header("Objects")]
    [SerializeField] private LayerMask groundLayer;

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Awake()
    {
        controls = new Controls();
        move = controls.Player.Move;
        jump = controls.Player.Jump;
        characterController = GetComponent<CharacterController>();
        serverUpdates = GetComponent<ServerUpdates>();
    }

    void Update()
    {
        if (!IsOwner) return;

        // Queue jump input for FixedUpdate
        if (jump.triggered)
        {
            jumpQueued = true;
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        // Check if grounded using CharacterController
        isGrounded = characterController.isGrounded;

        // Read movement input
        Vector2 input = move.ReadValue<Vector2>();
        Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        // Store movement direction when grounded (for use in the air)
        if (isGrounded)
        {
            lastGroundedMovementDirection = moveDirection; // Save direction when grounded
        }

        // Horizontal movement
        Vector3 horizontalVelocity = isGrounded ? moveDirection * movementSpeed : lastGroundedMovementDirection * movementSpeed;

        // Gravity and jump
        if (isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (jumpQueued)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        // Apply movement
        Vector3 finalVelocity = horizontalVelocity;
        finalVelocity.y = velocity.y;
        characterController.Move(finalVelocity * Time.fixedDeltaTime);

        // Update state
        isMoving = moveDirection != Vector3.zero;
        jumpQueued = false;

        serverUpdates.UpdatePositionServerRpc(transform.position);
    }

    // Getters and Setters
    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }

    public bool IsJumping
    {
        get => isJumping;
        set => isJumping = value;
    }

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }
}
