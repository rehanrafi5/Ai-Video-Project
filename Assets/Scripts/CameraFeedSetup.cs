using UnityEngine;

public class CameraFeedSetup : MonoBehaviour
{
    public Camera cam;
    public RenderTexture renderTexture;

    void Awake()
    {
        cam.targetTexture = renderTexture;
    }
}