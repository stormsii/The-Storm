using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class HealthbarScript : NetworkBehaviour
{
    [SerializeField] private Image hbFG;
    private PlayerStats _ps;

    private void Awake()
    {
        _ps = GetComponentInParent<PlayerStats>();
    }

    void Start()
    {
        hbFG.color = Color.green;
        UpdateHealthbar();
    }

    public void UpdateHealthbar()
    {
        hbFG.fillAmount = (float)_ps.currentHealth.Value/_ps.maxHealth.Value;
    }

    private void OnEnable()
    {
        _ps.currentHealth.OnValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _ps.currentHealth.OnValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        UpdateHealthbar();
    }

    public void ChangeHealthbarColor(bool targeted)
    {
        hbFG.color = targeted ? Color.red : Color.green;
    }

}
