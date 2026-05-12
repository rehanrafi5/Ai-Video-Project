using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public static ConveyorController Instance;

    public bool IsStopped { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void StopConveyor()
    {
        IsStopped = true;
        Debug.Log("🚨 Conveyor STOPPED (bad apple detected)");
    }

    public void ResumeConveyor()
    {
        IsStopped = false;
        Debug.Log("✅ Conveyor RESUMED");
    }
}