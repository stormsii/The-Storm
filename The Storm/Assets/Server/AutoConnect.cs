using Unity.Netcode;
using UnityEngine;

public class AutoConnect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Application.isBatchMode)
        {
            // Headless mode (server)
            NetworkManager.Singleton.StartServer();
            Debug.Log("Started as Server (Headless Mode)");
        }
        else
        {
            // If not headless, try host first if no one else is host
            if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                // Try starting as host
                bool hostStarted = NetworkManager.Singleton.StartHost();

                if (!hostStarted)
                {
                    NetworkManager.Singleton.StartClient();
                }
            }
        }
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton == null) return;
        NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log($"[AutoConnect] Client connected: {clientId}");
    }

}
