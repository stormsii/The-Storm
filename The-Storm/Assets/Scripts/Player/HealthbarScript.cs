using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class HealthbarScript : NetworkBehaviour
{
    [SerializeField] private Image hbFG;
    private PlayerStats _ps;
    private EnemyStats _es;
    private Canvas healthbarCanvas;

    private void Awake()
    {
        _ps = GetComponentInParent<PlayerStats>();
        _es = GetComponentInParent<EnemyStats>();
        healthbarCanvas = GetComponent<Canvas>();
    }
    
    void Start()
    {
        healthbarCanvas.enabled = false;
        hbFG.color = Color.green;
        UpdateHealthbar();
    }

    public void UpdateHealthbar()
    {
        if (_ps != null)
        {
            hbFG.fillAmount = (float)_ps.currentHealth.Value/_ps.maxHealth.Value;
        }

        if (_es != null)
        {
            hbFG.fillAmount = (float)_es.currentHealth.Value/_es.maxHealth.Value;
        }
    }

    private void OnEnable()
    {
        if (_ps != null)
        {
            _ps.currentHealth.OnValueChanged += OnHealthChanged;
        }
        if (_es != null)
        {
            _es.currentHealth.OnValueChanged += OnHealthChanged;
        }
        
    }

    private void OnDisable()
    {
        if (_ps != null)
        {
            _ps.currentHealth.OnValueChanged -= OnHealthChanged;
        }
        if (_es != null)
        {
            _es.currentHealth.OnValueChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(int oldValue, int newValue)
    {
        UpdateHealthbar();
    }

    public void ChangeHealthbarColor(bool targeted)
    {
        hbFG.color = targeted ? Color.red : Color.green;
    }

    public void ToggleHealthBar()
    {
        if (healthbarCanvas.enabled == true)
        {
            healthbarCanvas.enabled = false;
        }
        else
        {
            healthbarCanvas.enabled = true;
        }
    }

}
