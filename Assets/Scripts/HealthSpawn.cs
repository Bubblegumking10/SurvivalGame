using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPickupPrefab;
    public float spawnInterval = 20f;
    public float xRange = 40f;
    public float zRange = 40f;
    public float ySpawn = 1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHealth();
            timer = 0f;
        }
    }

    void SpawnHealth()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-xRange, xRange),
            ySpawn,
            Random.Range(-zRange, zRange)
        );

        Instantiate(healthPickupPrefab, spawnPos, Quaternion.identity);
    }
}
