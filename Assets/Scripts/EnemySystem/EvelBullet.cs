using UnityEngine;

public class EvelBullet : MonoBehaviour
{
   GameManager gameManager;
   AudioManager audioManager;
    private Rigidbody2D _rigidbody2D;
    [System.Obsolete]

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    [System.Obsolete]
    private void Start() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update() {
        Destroy(gameObject,3);
    }

   


    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag=="Astroids")
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.tag == "Player")
        {
            audioManager.PlayeSFX(audioManager.ShipHit);
            gameManager.PlayerTakeDamage(10);
            Destroy(gameObject);
        }

    }
}
