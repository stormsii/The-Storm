using UnityEngine;
using Unity.Netcode;
using UnityEditor.PackageManager;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform mainSpawnPoint;
    [SerializeField] private Renderer characterRenderer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

    private void SpawnPlayer(ulong clientId)
    {
        if (playerPrefab == null || mainSpawnPoint == null) { Debug.LogWarning("playerPrefab or mainSpawnPoint not found"); return; }

        Debug.Log($"Instantiating Character for Client [{clientId}]");
        GameObject player = Instantiate(playerPrefab, mainSpawnPoint.position, mainSpawnPoint.rotation);

        NetworkObject playerNO = player.GetComponent<NetworkObject>();
        if (playerNO != null)
        {
            playerNO.SpawnWithOwnership(clientId);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (IsServer)
        {
            SpawnPlayer(clientId);
        }
    }

}
