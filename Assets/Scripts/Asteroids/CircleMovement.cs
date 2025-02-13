using UnityEngine;
using System;

public class CircleMovement : MonoBehaviour
{
    public GameObject smallerAsteroidPrefab; // Assign a smaller asteroid prefab
    public int size = 3; // 3 = big, 2 = medium, 1 = small

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (size > 1)
            {
                SplitAsteroid();
            }

            // Destroy this asteroid
            Destroy(gameObject);
        }
    }

    void SplitAsteroid()
    {
        for (int i = 0; i < 2; i++) // Spawn two smaller asteroids
        {
            GameObject newAsteroid = Instantiate(smallerAsteroidPrefab, transform.position, Quaternion.identity);
            newAsteroid.GetComponent<CircleMovement>().size = size - 1; // Decrease the size

            // Give the smaller asteroids random velocities
            Rigidbody2D rb = newAsteroid.GetComponent<Rigidbody2D>();
            rb.linearVelocity = UnityEngine.Random.insideUnitCircle.normalized * 1f; // Adjust speed as needed
        }
    }
}
