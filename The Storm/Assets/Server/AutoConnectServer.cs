using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoConnectServer : MonoBehaviour
{
    void Start()
    {

        NetworkManager.Singleton.OnServerStarted += () =>
        {
            // Only load the gameplay scene on the server
            if (NetworkManager.Singleton.IsServer)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        };

        // Start the server if in headless mode
        NetworkManager.Singleton.StartServer();

    }
}
