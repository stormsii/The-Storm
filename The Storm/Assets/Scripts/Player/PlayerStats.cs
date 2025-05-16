using UnityEngine;
using Unity.Netcode;

public class PlayerStats : NetworkBehaviour
{
    [Header("Components")]
    private ServerUpdates serverUpdates;
    private HealthbarScript healthbarScript;

    [Header("Stats")]
    [SerializeField] public NetworkVariable<int> currentHealth = new NetworkVariable<int>(50);
    [SerializeField] public NetworkVariable<int> maxHealth = new NetworkVariable<int>(50);
    [SerializeField] private int currentPower = 50; public int CurrentPower { get => currentPower; set => currentPower = value; }
    [SerializeField] private int maxPower = 100; public int MaxPower { get => maxPower; set => maxPower = value; }
    [SerializeField] private int currentSecondaryPower = 50; public int CurrentSecondaryPower { get => currentSecondaryPower; set => currentSecondaryPower = value; }
    [SerializeField] private int maxSecondaryPower = 100; public int MaxSecondaryPower { get => maxSecondaryPower; set => maxSecondaryPower = value; }
    [SerializeField] private int currentLevel = 1; public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    [Header("Target")]
    [SerializeField] private Targeting currentTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serverUpdates = GetComponent<ServerUpdates>();
        healthbarScript = GetComponentInChildren<HealthbarScript>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
