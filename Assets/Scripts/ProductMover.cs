using UnityEngine;

public class ProductMover : MonoBehaviour
{
    public TrayPath path;
    public float speed = 2f;

    private int currentIndex = 0;

    void Start()
    {
        if (path != null && path.waypoints.Length > 0)
        {
            transform.position = path.waypoints[0].position;
            currentIndex = 1; // move toward next point
        }
    }

    void Update()
    {
        if (ConveyorController.Instance != null && ConveyorController.Instance.IsStopped)
            return;

        if (path == null || path.waypoints.Length < 2) return;

        Transform target = path.waypoints[currentIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        Vector3 dir = (target.position - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, 10f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;

            if (currentIndex >= path.waypoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}