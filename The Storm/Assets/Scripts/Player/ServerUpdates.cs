using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class ServerUpdates : NetworkBehaviour
{
    private PlayerStats playerStats;
    private HealthbarScript healthbarScript;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    [ServerRpc]
    public void UpdatePositionServerRpc(Vector3 newPosition)
    {
        // Update position on the server
        transform.position = newPosition;

        // Sync the position across all clients
        UpdatePositionClientRpc(newPosition);
    }

    // Client-side position update (called after server updates the position)
    [ClientRpc]
    public void UpdatePositionClientRpc(Vector3 newPosition)
    {
        // Only update position on clients that are not the owner (i.e., other players)
        if (!IsOwner)
        {
            // Optional: Apply smoothing/interpolation to make movement smoother
            transform.position = newPosition;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateHealthServerRpc(int healthChange)
    {
        if (healthChange == 0) { return; }

        // Update position on the server
        playerStats.currentHealth.Value -= healthChange;
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeHealthServerRpc(NetworkObjectReference targetRef, float healthChange)
    {
        if (healthChange == 0) { return; }

        if (targetRef.TryGet(out NetworkObject netObj))
        {
            // Debugging ownership before attempting to change health

            if (netObj.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.ChangeHealth((int)healthChange);
            }
            else
            {
                Debug.LogError("Target does not have IDamageable component.");
            }
        }
        else
        {
            Debug.LogError("Failed to get NetworkObject from target reference.");
        }
    }

}
