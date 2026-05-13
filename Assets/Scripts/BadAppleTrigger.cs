using UnityEngine;

public class BadAppleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ProductData data = other.GetComponent<ProductData>();

        if (data == null) return;

        // 🚨 bad product → go inspection flow
        if (data.isBad)
        {
            if (ConveyorController.Instance != null)
            {
                ProductLogManager.instance.AddProduct(data);
                
                ConveyorController.Instance.HandleBadAppleDetected(other.gameObject);
            }
        }
        else
        {
            // 🟢 GOOD PRODUCT → log immediately
            ProductLogManager.instance.AddProduct(data);
        }
    }
}