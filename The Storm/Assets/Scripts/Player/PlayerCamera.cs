using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    [Header("Cursor")]
    public RectTransform cursorRect;

    [Header("Component References")]
    private GameObject player;
    private Controls controls;

    [Header("Controls")]
    private InputAction look;
    private InputAction leftClick;
    private InputAction rightClick;

    [Header("Variables")]
    [SerializeField] private float rotationSpeed;
    private Vector3 offset;
    private Quaternion rotation;

    private Vector3 lastCursorPos;
    private bool invisibleCursor = false;

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
        //Disable/Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        player = transform.parent.gameObject;

        controls = new Controls();
        look = controls.Player.Look;
        leftClick = controls.Player.LeftClick;
        rightClick = controls.Player.RightClick;
    }

    void Start()
    {
        offset = new Vector3(0, 5, -10);
        rotation = Quaternion.Euler(20f, 0f, 0f);
        transform.position = player.transform.position + offset;
        transform.rotation = rotation;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        cursorRect.position = mousePos;
    }

    void LateUpdate()
    {

        /*
        if (!IsOwner) { Debug.Log("Not Owner!"); return; }
        

        if (leftClick.IsPressed() && !invisibleCursor)
        {
            InvisibleCursor();
            Debug.Log("Left Pressed");
        }
        else
        {
            if (invisibleCursor)
            {
                VisibleCursor();
            }
        }
        
    }

    void InvisibleCursor()
    {
        invisibleCursor = true;
        Vector3 mousePosition = Input.mousePosition;  // Store screen space position directly
        lastCursorPos = mousePosition;  // You can use this for tracking the cursor's position in screen space
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
    }

    void VisibleCursor()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor and allow it to move freely
        Cursor.visible = true;
        invisibleCursor = false;
    }
        */
    }
}