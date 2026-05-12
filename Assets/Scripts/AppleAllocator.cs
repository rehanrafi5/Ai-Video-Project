using UnityEngine;
using System.Collections.Generic;

public class AppleAllocator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject redApplePrefab;
    public GameObject greenApplePrefab;

    [Header("Slots (positions inside tray)")]
    public Transform[] slots;

    [Header("Green Apple Range")]
    public int minGreen = 2;
    public int maxGreen = 7;

    void Start()
    {
        AllocateApples();
    }

    public void AllocateApples()
    {
        if (slots == null || slots.Length == 0) return;

        // Clamp in case slots < maxGreen
        int greenCount = Random.Range(minGreen, Mathf.Min(maxGreen, slots.Length) + 1);

        // Create shuffled slot indices
        List<int> indices = new List<int>();
        for (int i = 0; i < slots.Length; i++)
            indices.Add(i);

        Shuffle(indices);

        // Assign apples
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject prefabToSpawn = (i < greenCount) ? greenApplePrefab : redApplePrefab;

            Instantiate(
                prefabToSpawn,
                slots[indices[i]].position,
                slots[indices[i]].rotation,
                transform // parent to tray
            );
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}