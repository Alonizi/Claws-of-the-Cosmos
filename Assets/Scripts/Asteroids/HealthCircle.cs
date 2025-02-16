using System;
using UnityEngine;

public class HealthCircle : MonoBehaviour
{

    private GameManager Manager; 
    private void Start()
    {
         Manager = FindAnyObjectByType<GameManager>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Manager != null)
            {
                Manager.PlayerHeal(25);
            }

            Debug.Log("HealthCircle collected"); // Debugging line
            Destroy(gameObject); // Remove the health collectible after collection
        }
        else if (other.CompareTag("Bullet")) // If shot, destroy it but don't increase score
        {
            Debug.Log("HealthCircle destroyed by bullet (No Score)");
            //Destroy(gameObject);
        }
    }
}
