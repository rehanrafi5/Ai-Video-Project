using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject fineApplePrefab;
    public GameObject notFineApplePrefab;

    public TrayPath path;

    public float spawnInterval = 2f;

    [Range(0f, 1f)]
    public float fineChance = 0.97f; // 97% fine

    private float timer;

    void Update()
    {
        if (path == null || path.waypoints.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnProduct();
            timer = 0f;
        }
    }

    void SpawnProduct()
    {
        // 🎯 Decide which apple to spawn
        GameObject prefabToSpawn;

        float roll = Random.value; // 0 → 1

        if (roll <= fineChance)
        {
            prefabToSpawn = fineApplePrefab;
        }
        else
        {
            prefabToSpawn = notFineApplePrefab;
        }

        // Spawn
        GameObject obj = Instantiate(
            prefabToSpawn,
            path.waypoints[0].position,
            path.waypoints[0].rotation
        );

        // Assign movement
        ProductMover mover = obj.GetComponent<ProductMover>();
        if (mover != null)
            mover.path = path;
    }
}