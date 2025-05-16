using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (_mainCam == null) return;

        transform.forward = _mainCam.transform.forward;
    }
}
