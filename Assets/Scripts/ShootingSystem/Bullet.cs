using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] float bulletSpeed;
    private Rigidbody2D _rigidbody2D;
   

    [System.Obsolete]

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update() {
        Destroy(gameObject,2);
    }

        public void Project(Vector2 direction ){
       _rigidbody2D.AddForce(direction * bulletSpeed);
       
    }
   

private void OnCollisionEnter(Collision other) {
    Destroy(gameObject);
}
 
}
