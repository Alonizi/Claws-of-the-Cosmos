using UnityEngine;

public class HealthCircle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                
            }

            Debug.Log("HealthCircle collected"); // Debugging line
            Destroy(gameObject); // Remove the health collectible after collection
        }
        else if (other.CompareTag("Bullet")) // If shot, destroy it but don't increase score
        {
            Debug.Log("HealthCircle destroyed by bullet (No Score)");
            Destroy(gameObject);
        }
    }
}
