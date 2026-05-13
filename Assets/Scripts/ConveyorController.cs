using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class ConveyorController : MonoBehaviour
{
    public static ConveyorController Instance;


    [Header("Refs")]
    public CameraSwitcher cameraSwitcher;
    public int machineCameraIndex = 1;
    public ProductSpawner spawner;

    [Header("UI")]
    public GameObject decisionPanel;

    [Header("Cutscene")]
    public PlayableDirector cutsceneDirector; // where item moves after cut

    private GameObject currentDetectedItem;
    private ProductData currentData;

    public GameObject Trigger;

    void Awake()
    {
        Instance = this;
    }

    // 🚨 Called from trigger
    public void HandleBadAppleDetected(GameObject item)
    {
        currentDetectedItem = item;
        currentData = item.GetComponent<ProductData>();

        if (cameraSwitcher != null)
            cameraSwitcher.SwitchToCamera(machineCameraIndex);

        RejectProduct();
    }
    
    public void ApproveProduct()
    {
        if (ProductLogManager.instance != null && currentData != null)
        {
            ProductLogManager.instance.AddProduct(currentData);
        }

        ResumeSystem();
    }

    // ❌ REJECT (play cutscene + remove item)
    public void RejectProduct()
    {
        if (currentData != null && ProductLogManager.instance != null)
        {
            ProductLogManager.instance.AddProduct(currentData);
        }

        if (currentDetectedItem != null)
        {
            StartCoroutine(HandleRejectSequence());
        }
        else
        {
            ResumeSystem();
        }
    }
    

    IEnumerator HandleRejectSequence()
    {
        decisionPanel.SetActive(false);

        ResumeSystem();
        Trigger.SetActive(true);

        // 🐢 SLOW ALL MOVEMENT
        ProductSpawner.spawnMultiplier = 0.2f; // slow spawn
        ProductMover.globalSpeedMultiplier = 0.2f; // slow movement

        // ❗ DISABLE ALL NORMAL CAMERAS FIRST
        if (cameraSwitcher != null)
        {
            cameraSwitcher.DisableAllCameras();
        }

        // 🎬 Play cutscene
        if (cutsceneDirector != null)
        {
            cutsceneDirector.gameObject.SetActive(true);
            cutsceneDirector.Play();

            yield return new WaitForSeconds(4.2f);
        }
        
        ProductSpawner.spawnMultiplier = 1f;
        ProductMover.globalSpeedMultiplier = 1f;

        // 🎥 TURN OFF CUTSCENE CAMERA
        if (cutsceneDirector != null)
        {
            Camera cutCam = cutsceneDirector.GetComponentInChildren<Camera>();
            if (cutCam != null)
                cutCam.gameObject.SetActive(false);
        }

        // ⚡ RESTORE SPEED AFTER ROUTINE
        
        cameraSwitcher.SwitchToCamera(0);

        Trigger.SetActive(false);
    }
    
    void ResumeSystem()
    {

        if (cameraSwitcher != null)
            cameraSwitcher.SwitchToCamera(0);

        if (decisionPanel != null)
            decisionPanel.SetActive(false);

        currentDetectedItem = null;
        currentData = null;
    }
}