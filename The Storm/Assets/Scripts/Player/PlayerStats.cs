using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int currentHealth = 50; public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    [SerializeField] private int maximumHealth = 100; public int MaximumHealth { get => maximumHealth; set => maximumHealth = value; }
    [SerializeField] private int currentPower = 50; public int CurrentPower { get => currentPower; set => currentPower = value; }
    [SerializeField] private int maxPower = 100; public int MaxPower { get => maxPower; set => maxPower = value; }
    [SerializeField] private int currentSecondaryPower = 50; public int CurrentSecondaryPower { get => currentSecondaryPower; set => currentSecondaryPower = value; }
    [SerializeField] private int maxSecondaryPower = 100; public int MaxSecondaryPower { get => maxSecondaryPower; set => maxSecondaryPower = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int health)
    {
        currentHealth -= health;
    }
}
