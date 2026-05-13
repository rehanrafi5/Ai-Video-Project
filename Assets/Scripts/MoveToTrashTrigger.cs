using UnityEngine;

public class MoveToTrashTrigger : MonoBehaviour
{
    public TrayPath trashPath;

    private void OnTriggerEnter(Collider other)
    {
        ProductData data = other.GetComponent<ProductData>();
        if (data == null) return;

        if (data.isBad)
        {
            ProductMover mover = other.GetComponent<ProductMover>();

            if (mover != null && trashPath != null)
            {
                mover.SetPath(trashPath);
            }
        }
    }
}