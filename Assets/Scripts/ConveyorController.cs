using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public static ConveyorController Instance;

    public bool IsStopped { get; private set; }

    [Header("Refs")]
    public CameraSwitcher cameraSwitcher;
    public int machineCameraIndex = 1;
    public ProductSpawner spawner;

    [Header("UI")]
    public GameObject decisionPanel;

    private GameObject currentDetectedItem;
    private ProductData currentData;

    void Awake()
    {
        Instance = this;
    }

    // 🚨 Called from trigger
    public void HandleBadAppleDetected(GameObject item)
    {
        IsStopped = true;

        currentDetectedItem = item;
        currentData = item.GetComponent<ProductData>();

        if (spawner != null)
            spawner.isStopped = true;

        if (cameraSwitcher != null)
            cameraSwitcher.SwitchToCamera(machineCameraIndex);

        if (decisionPanel != null)
            decisionPanel.SetActive(true);
    }

    // ✅ APPROVE BUTTON (GOOD PRODUCT)
    public void ApproveProduct()
    {
        if (ProductLogManager.instance != null && currentData != null)
        {
            ProductLogManager.instance.AddProduct(currentData);
        }

        ResumeSystem();
    }

    // ❌ REJECT BUTTON (BAD PRODUCT)
    [ContextMenu("RejectProduct")]
    public void RejectProduct()
    {
        if (currentData != null && ProductLogManager.instance != null)
        {
            ProductLogManager.instance.AddProduct(currentData);
        }

        if (currentDetectedItem != null)
        {
            Destroy(currentDetectedItem);
        }

        ResumeSystem();
    }

    void ResumeSystem()
    {
        IsStopped = false;

        if (spawner != null)
            spawner.isStopped = false;

        if (cameraSwitcher != null)
            cameraSwitcher.SwitchToCamera(0);

        if (decisionPanel != null)
            decisionPanel.SetActive(false);

        currentDetectedItem = null;
        currentData = null;
    }
}