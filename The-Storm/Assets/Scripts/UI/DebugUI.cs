using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [Header("Components")]
    private MovementController _mc;
    private PlayerStats _ps;

    [SerializeField] private TMP_Text debugText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mc = GetComponentInParent<MovementController>();
        _ps = GetComponentInParent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

        debugText.text =
            $"<b>Movement</b>" +
            $"\nPosition: {_mc.transform.position}" +
            $"\nVelocity: {_mc.Velocity}" +
            $"\nisMoving: {_mc.IsMoving}" +
            $"\nisGrounded: {_mc.IsGrounded}" +
            $"\nisJumping: {_mc.IsJumping}" +
            $"\nMovementSpeed: {_mc.MovementSpeed}" +
            $"\n\n<b>Player Stats</b>" +
            $"\nHealth: {_ps.currentHealth.Value}/{_ps.maxHealth.Value}" +
            $"\nPower: {_ps.CurrentPower}/{_ps.MaxPower}" +
            $"\nSecondaryPower: {_ps.CurrentSecondaryPower}/{_ps.MaxSecondaryPower}";
    }

    public void ToggleDebugMenu()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
