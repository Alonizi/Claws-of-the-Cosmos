using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    
    private Vector3 MouseDirection;
    public float BulletSpeed = 2f;
    public float FireRate = 0.2f;
    private float TimeCounter;
    private float ShipAxis ;
    AudioManager audioManager; 

    private void Start() {
         audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        TimeCounter += Time.deltaTime;
        if (gameObject.tag == "Player")
        {
            MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            ShipAxis = AimShip();
            Fire(ShipAxis);
        }
    }
    
    private float AimShip()
    {
        var angle = (Mathf.Rad2Deg * Mathf.Atan2(MouseDirection.y, MouseDirection.x)) - 90;
        //Debug.DrawRay(objectPosition,GetMouseDirection(),Color.cyan);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return angle;
    }

    public void Fire(float angle)
    {
        if (Input.GetAxisRaw("Fire1") == 1  || Input.GetKey(KeyCode.Space))
        {
            audioManager.PlayeSFX(audioManager.DieClip);
            TimeCounter += Time.deltaTime;
            if (TimeCounter > FireRate)
            {
                var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, angle));
                bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * MouseDirection;
                TimeCounter = 0; 
            }
        }
    }

}
