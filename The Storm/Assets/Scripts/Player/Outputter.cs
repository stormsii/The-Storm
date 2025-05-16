using Unity.Netcode;
using UnityEngine;

public class Outputter : NetworkBehaviour
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

    public void ChangeHealth(GameObject target, Spell spell)
    {
        if (target.TryGetComponent<NetworkObject>(out var netObj))
        {
            NetworkObjectReference targetRef = new NetworkObjectReference(netObj);

            _su.ChangeHealthServerRpc(targetRef, spell.damage);

        }
    }

}
