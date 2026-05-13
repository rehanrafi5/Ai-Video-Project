using System.Collections.Generic;
using UnityEngine;

public class ProductMover : MonoBehaviour
{
    public static List<ProductMover> allMovers = new List<ProductMover>();

    public TrayPath path;
    public float speed = 2f;

    public static float globalSpeedMultiplier = 1f;

    private int currentIndex = 0;

    void OnEnable()
    {
        allMovers.Add(this);
    }

    void OnDisable()
    {
        allMovers.Remove(this);
    }

    void Start()
    {
        if (path != null && path.waypoints.Length > 0)
        {
            transform.position = path.waypoints[0].position;
            currentIndex = 1;
        }
    }

    void Update()
    {

        if (path == null || path.waypoints.Length < 2) return;

        Transform target = path.waypoints[currentIndex];

        float finalSpeed = speed * globalSpeedMultiplier;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            finalSpeed * Time.deltaTime
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

    public void SetPath(TrayPath newPath)
    {
        path = newPath;
        currentIndex = 0;

        if (path != null && path.waypoints.Length > 0)
        {
            transform.position = path.waypoints[0].position;
            currentIndex = 1;
        }
    }
}