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
    private Targeting _t;
    private Damageable _d;
    private Outputter _o;
    private Spellbook _sb;
    private Casting _c;

    private Controls controls;

    [Header("Controls")]
    private InputAction toggleDebug;
    private InputAction testDamage;
    private InputAction clickTarget;
    private InputAction escape;

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

        testDamage = controls.Player.TestDamage;
        toggleDebug = controls.UI.ToggleDebugMenu;
        clickTarget = controls.Player.LeftClick;
        escape = controls.Player.Escape;
    }

    void Start()
    {
        _ps = GetComponent<PlayerStats>();
        _dUI = GetComponentInChildren<DebugUI>(true);
        _t = GetComponent<Targeting>();
        _d = GetComponent<Damageable>();
        _o = GetComponent<Outputter>();
        _sb = GetComponent<Spellbook>();
        _c = GetComponent<Casting>();
    }

    void Update()
    {
        if (!IsOwner) return;
        if (testDamage.triggered) { _c.Cast(_sb.knownSpells[0]); Debug.Log($"[PlayerStats] {_sb.knownSpells[0].damage} Damage tried"); }
        if (toggleDebug.triggered) {_dUI.ToggleDebugMenu();}
        if (clickTarget.triggered) { Click(); }
        if (escape.triggered) { _t.Untarget(); }
    }

    void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the target tag
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Hit target: " + hit.collider.name);

                // Call the OnTarget() method or any other logic you want to execute
                Targeting target = hit.collider.gameObject.GetComponent<Targeting>();
                if (target != null)
                {
                    _t.Target(target);
                }
            }
        }
    }

}
