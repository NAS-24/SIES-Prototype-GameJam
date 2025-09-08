using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroids Setup")]
    public GameObject[] asteroidPrefabs; // assign in Inspector
    public Transform ship;               // assign your ship here

    [Header("Spawn Settings")]
    public float spawnRate = 1f;
    public float xRange = 10f;
    public float ySpawn = 15f;

    private float timer = 0f;
    private bool canSpawn = true; // control spawning

    void Update()
    {
        if (!canSpawn) return; // stop spawning when disabled

        timer += Time.deltaTime;
        if (timer >= 1f / spawnRate)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length == 0) return; // safety check

        // pick random asteroid
        int index = Random.Range(0, asteroidPrefabs.Length);
        GameObject prefab = asteroidPrefabs[index];

        // spawn position
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), ySpawn, 0);

        // instantiate
        GameObject asteroid = Instantiate(prefab, spawnPos, Quaternion.identity);

        // add Rigidbody2D if not already present
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb == null) rb = asteroid.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // move downward
        rb.linearVelocity = new Vector2(0, -Random.Range(2f, 6f));

        // spin
        rb.angularVelocity = Random.Range(-90f, 90f);
    }

    // Call this when zoom-out starts
    public void StopSpawning()
    {
        canSpawn = false;
    }
}
