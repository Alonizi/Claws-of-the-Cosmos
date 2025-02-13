using System;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject HealthCirclePrefab; // Assign this in Unity
    public float healthSpawnChance = 0.2f; // 20% chance of spawning
    public GameObject AsteroidPrefab;
    public float SpawnRate = 2f; // Time between spawns
    public float SpawnDistance = 10f; // Distance outside the screen to spawn
    public float AsteroidSpeed = 2f; // Speed of the asteroids

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnRate)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    /// <summary>
    /// Spawns an object (either Asteroid or HealthCircle) outside the screen with natural movement.
    /// </summary>
    void SpawnObject()
    {
        // Pick a random direction from outside the screen
        Vector2 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized;
        Vector2 spawnPoint = spawnDirection * SpawnDistance;

        // Lower spawn chance for HealthCircles (e.g., 10% chance instead of 20%)
        if (UnityEngine.Random.value < healthSpawnChance * 0.5f) // Reduce the chance by half
        {
            Debug.Log("Spawning HealthCircle"); // Debugging line
            SpawnMovingObject(HealthCirclePrefab, spawnPoint, spawnDirection);
        }
        else
        {
            SpawnMovingObject(AsteroidPrefab, spawnPoint, spawnDirection);
        }
    }


    void SpawnMovingObject(GameObject prefab, Vector2 spawnPoint, Vector2 spawnDirection)
    {
        GameObject obj = Instantiate(prefab, spawnPoint, Quaternion.identity);
        
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();


        // Add movement with slight randomness to make it more dynamic
        Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f));
        rb.linearVelocity = (-spawnDirection + randomOffset).normalized * AsteroidSpeed;
    }


    /// <summary>
    /// Spawns an asteroid at a given position with smooth movement.
    /// </summary>
    void SpawnAsteroid(Vector2 spawnPoint, Vector2 spawnDirection)
    {
        GameObject asteroid = Instantiate(AsteroidPrefab, spawnPoint, Quaternion.identity);
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();

        // Add movement with random variation
        Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f));
        rb.linearVelocity = (-spawnDirection + randomOffset).normalized * AsteroidSpeed;
    }

    /// <summary>
    /// Spawns a HealthCircle at a given position with natural movement.
    /// </summary>
    void SpawnHealthCircle(Vector2 spawnPoint, Vector2 spawnDirection)
    {
        GameObject healthCircle = Instantiate(HealthCirclePrefab, spawnPoint, Quaternion.identity);
        Rigidbody2D rb = healthCircle.GetComponent<Rigidbody2D>();

        // Add movement with slight randomness
        Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f));
        rb.linearVelocity = (-spawnDirection + randomOffset).normalized * AsteroidSpeed;
    }
}
