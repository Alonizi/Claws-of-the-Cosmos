// copyrights Abdulaziz Alonizi 2025
using UnityEngine;

/// <summary>
/// Control Weapon Mechanism for Player's Ship
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private float BulletSpeed = 2f;
    [SerializeField] private float FireRate = 0.2f;

    private Vector3 MouseDirection;
    private float TimeCounter;
    private float ShipAxis ;
    
    /// <summary>
    /// responsible for updating mouse position , ship rotation
    /// and calling the Fire mechanism
    /// </summary>
    void Update()
    {
            MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            ShipAxis = AimShip();
            Fire(ShipAxis);
    }
    
    /// <summary>
    /// rotates the ship based on the direction of the mouse
    /// </summary>
    private float AimShip()
    {
        var angle = (Mathf.Rad2Deg * Mathf.Atan2(MouseDirection.y, MouseDirection.x)) - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return angle;
    }

    /// <summary>
    /// Register user's input and fire a bullet at the specified angle
    /// </summary>
    /// <param name="angle"> the angle at which to fire the projectile</param>
    public void Fire(float angle)
    {
        if (Input.GetAxisRaw("Fire1") == 1  || Input.GetKey(KeyCode.Space))
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > FireRate)
            {
                var bullet = Instantiate(
                    BulletPrefab,
                    transform.GetChild(0).position,
                    Quaternion.Euler(0, 0, angle)
                    );
                Vector2 bulletDirection = MouseDirection.normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * bulletDirection;
                TimeCounter = 0; 
            }
        }
    }
}
