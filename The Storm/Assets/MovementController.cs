using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class MovementController : NetworkBehaviour
{
    [Header("Component References")]
    private CharacterController characterController;
    private Controls controls;
    private InputAction move;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isJumping;

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

        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        /*
        if (!IsOwner) { Debug.Log("Not Owner!"); return; }
        */
        
        Vector2 input = move.ReadValue<Vector2>();
        Vector3 moving = new Vector3(input.x, 0f, input.y);

        if (moving != Vector3.zero)
        {
            characterController.Move(moving * movementSpeed * Time.deltaTime);
            isMoving = true;

            UpdatePositionServerRpc(moving);
        }
        else
        {
            isMoving = false;
        }
    }

    [ServerRpc]
    private void UpdatePositionServerRpc(Vector3 moving)
    {
        transform.position = moving;
    }

    // getters & setters
    public float MovementSpeed
    {
        get => movementSpeed;
        set
        {
            movementSpeed = 0f;
        }
    }
    public bool IsMoving
    {
        get => isMoving;
        set
        {
            isMoving = value;
        }
    }
    public bool IsJumping
    {
        get => isJumping;
        set
        {
            isJumping = value;
        }
    }
}
