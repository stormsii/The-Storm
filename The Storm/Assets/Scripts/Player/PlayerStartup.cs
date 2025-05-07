using UnityEngine;
using Unity.Netcode;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField] private Renderer characterRenderer;

    private void Awake()
    {

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (characterRenderer == null) return;

        if (OwnerClientId == 0)
        {
            SetColor(Color.red); // Player 1 (host)
        }
        else
        {
            SetColor(Color.blue); // Player 2+
        }

        // Ensure only the local player's camera has the AudioListener enabled
        if (IsOwner)
        {
            EnableAudioListener();
            EnableCamera();
        }
        else
        {
            DisableAudioListener();
            DisableCamera();
        }
    }

    private void SetColor(Color color)
    {
        characterRenderer.material.color = color;
    }

    private void EnableAudioListener()
    {
        AudioListener audioListener = GetComponent<AudioListener>();
        if (audioListener != null)
        {
            audioListener.enabled = true;
        }
    }

    private void DisableAudioListener()
    {
        AudioListener audioListener = GetComponent<AudioListener>();
        if (audioListener != null)
        {
            audioListener.enabled = false;
        }
    }

    private void EnableCamera()
    {
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            cam.enabled = true;
        }
    }

    private void DisableCamera()
    {
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            cam.enabled = false;
        }
    }

}
