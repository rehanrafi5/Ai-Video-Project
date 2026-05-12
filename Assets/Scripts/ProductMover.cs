using UnityEngine;

public class ProductMover : MonoBehaviour
{
    public TrayPath path;
    public float speed = 2f;

    private int currentIndex = 0;

    void Update()
    {
        if (path == null || path.waypoints.Length == 0) return;

        Transform target = path.waypoints[currentIndex];

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Rotate toward movement direction (optional but nice)
        Vector3 dir = (target.position - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, 10f * Time.deltaTime);
        }

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;

            // Loop OR destroy at end
            if (currentIndex >= path.waypoints.Length)
            {
                // Option 1: loop
                currentIndex = 0;

                // Option 2: destroy instead
                // Destroy(gameObject);
            }
        }
    }
}