using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    [SerializeField] private GameObject BulletPrefab;

    public float BulletSpeed = 2f;
    public float FireRate = 0.2f;
    private float TimeCounter = 0;
    private Vector3 MouseDirection;

   private float ShipAxis = 0;

    //------Auto Bullets-------
    public Vector3 bulletSpawn;


    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;
        
        MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        ShipAxis = Aim();
        Fire(ShipAxis);
        
    }

    private float Aim()
    {
        var z = (Mathf.Rad2Deg * Mathf.Atan2(MouseDirection.y, MouseDirection.x)) - 90;
        
        transform.rotation = Quaternion.Euler(0, 0, z);
        return z;
    }

    public void Fire(float angle)
    {
        if (Input.GetAxisRaw("Fire1") == 1  || Input.GetKey(KeyCode.Space))
        {
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
