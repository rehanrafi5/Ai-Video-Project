using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject fineApplePrefab;
    public GameObject[] notFineApplePrefabs;

    public TrayPath path;

    public float spawnInterval = 2f;

    private float timer;

    private int fineCountTarget; // how many fine before bad
    private int currentFineCount = 0;
    
    public bool isStopped = false;

    void Start()
    {
        SetNextBatch();
    }

    void Update()
    {
        if (isStopped) return;

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
        GameObject prefabToSpawn;

        // 🎯 If reached target → spawn bad apple
        if (currentFineCount >= fineCountTarget)
        {
            if (notFineApplePrefabs != null && notFineApplePrefabs.Length > 0)
            {
                int randIndex = Random.Range(0, notFineApplePrefabs.Length);
                prefabToSpawn = notFineApplePrefabs[randIndex];
            }
            else
            {
                Debug.LogWarning("No not-fine prefabs assigned!");
                return;
            }

            currentFineCount = 0;
            SetNextBatch();
        }
        else
        {
            prefabToSpawn = fineApplePrefab;
            currentFineCount++;
        }

        GameObject obj = Instantiate(
            prefabToSpawn,
            path.waypoints[0].position,
            path.waypoints[0].rotation
        );

        ProductMover mover = obj.GetComponent<ProductMover>();
        if (mover != null)
            mover.path = path;
    }

    void SetNextBatch()
    {
        fineCountTarget = Random.Range(3, 6); // 4 to 6
    }
}