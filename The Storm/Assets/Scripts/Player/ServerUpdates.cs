using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class ServerUpdates : NetworkBehaviour
{
    [ServerRpc]
    private void UpdatePositionServerRpc(Vector3 newPosition)
    {
        // Update position on the server
        transform.position = newPosition;

        // Sync the position across all clients
        UpdatePositionClientRpc(newPosition);
    }

    // Client-side position update (called after server updates the position)
    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 newPosition)
    {
        // Only update position on clients that are not the owner (i.e., other players)
        if (!IsOwner)
        {
            // Optional: Apply smoothing/interpolation to make movement smoother
            transform.position = newPosition;
        }
    }

}
