using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    private int currentIndex = 0;

    void Start()
    {
        ActivateCamera(currentIndex);
    }

    void Update()
    {
        // Press C to switch to next camera
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextCamera();
        }
    }

    public void NextCamera()
    {
        currentIndex++;

        if (currentIndex >= cameras.Length)
            currentIndex = 0;

        ActivateCamera(currentIndex);
    }

    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}