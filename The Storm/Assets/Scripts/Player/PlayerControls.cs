using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerControls : NetworkBehaviour
{
    [Header("Component")]
    private MovementController _mc;
    private PlayerCamera _pc;
    private DebugUI _dUI;
    private PlayerStats _ps;

    private Controls controls;

    [Header("Controls")]
    private InputAction toggleDebug;
    private InputAction testDamage;

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new Controls();

        testDamage = controls.Player.Interact;
        toggleDebug = controls.UI.ToggleDebugMenu;
    }

    void Start()
    {
        _ps = GetComponent<PlayerStats>();
        _dUI = GetComponentInChildren<DebugUI>(true);
    }

    void Update()
    {
        if (!IsOwner) return;
        if (testDamage.triggered) { _ps.ChangeHealth(10); Debug.LogWarning("[PlayerStats] 10 Damage tried");}
        if (toggleDebug.triggered) {_dUI.ToggleDebugMenu();}
    }
}
