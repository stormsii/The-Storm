using Unity.Netcode;
using UnityEngine;

public class AutoConnectClient : MonoBehaviour
{
    [SerializeField] private bool isHost;

    void Start()
    {
        if(!isHost)
        {
            NetworkManager.Singleton.StartClient();
            
        }
        else
        {
            NetworkManager.Singleton.StartHost();
        } 
    }
}
