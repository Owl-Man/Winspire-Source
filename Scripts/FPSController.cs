using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private byte FPS = 60;
    [SerializeField] private bool isVSyncActive;

    private void Start()
    {
        QualitySettings.vSyncCount = !isVSyncActive ? 0 : 1;
        Application.targetFrameRate = FPS;
    }
}
