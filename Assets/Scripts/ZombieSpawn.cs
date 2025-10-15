using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;  // Assign your zombie prefab
    public float spawnIntervalMin = 5f;
    public float spawnIntervalMax = 15f;
    public float spawnRange = 50f;  // Area around the spawner to spawn zombies

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPos = transform.position + new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        }
    }
}

