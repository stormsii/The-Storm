using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.Netcode;
using TMPro;
using System.Globalization;

public class PlayerCamera : NetworkBehaviour
{
    [Header("UI Raycasting")]
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

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

    private float yaw = 0f;
    private float pitch = 20f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 80f;

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
        look = controls.Player.Look;
        leftClick = controls.Player.LeftClick;
        rightClick = controls.Player.RightClick;

        player = transform.parent.gameObject;
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

    }

    void LateUpdate()
    {
        if (!IsOwner) return;

        // Rotate camera and player based on mouse movement when holding right-click (WoW-style)
        if (rightClick.IsPressed() && !IsPointerOverUI())
        {
            // Capture the mouse movement delta
            Vector2 mouseDelta = look.ReadValue<Vector2>();

            // Update yaw based on mouse X movement
            yaw += mouseDelta.x * rotationSpeed * Time.deltaTime;

            // Update pitch based on mouse Y movement (clamped to min/max)
            pitch -= mouseDelta.y * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            // Rotate the player around the Y-axis (horizontal) with the camera's yaw
            player.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }

        // When left-click is held, just rotate the camera around the player (no player rotation)
        else if (leftClick.IsPressed() && !IsPointerOverUI())
        {
            Vector2 mouseDelta = look.ReadValue<Vector2>();

            // Update yaw for smooth horizontal camera rotation
            yaw += mouseDelta.x * rotationSpeed * Time.deltaTime;

            // Update pitch for smooth vertical camera movement
            pitch -= mouseDelta.y * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Calculate camera’s final rotation (based on yaw and pitch)
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Orbit the camera around the player
        Vector3 rotatedOffset = targetRotation * offset;
        transform.position = player.transform.position + rotatedOffset;

        // Always make the camera look at the player
        transform.LookAt(player.transform.position + Vector3.up * 1.5f); // Adjust height if needed
    }


    private bool IsPointerOverUI()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);
        
        return results.Count > 0;
    }

}