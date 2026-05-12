using UnityEngine;

public class BadAppleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if it's a bad apple
        if (other.CompareTag("BadApple"))
        {
            if (ConveyorController.Instance != null)
            {
                ConveyorController.Instance.StopConveyor();
            }
        }
    }
}