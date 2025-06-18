using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{

    private ServerUpdates _su;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _su = GetComponent<ServerUpdates>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int healthChange)
    {
        _su.UpdateHealthServerRpc(healthChange);
    }
}
