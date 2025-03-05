//copyrights Abdulaziz Alonizi 2025
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// UFO Script for an enemy ship rotates around its axis
    /// and shoots multiple bullets Diagonally
    /// </summary>
    public class Ufo:VehicleController,IVehicle,IWeapon
    {
        [SerializeField] public GameObject BulletPrefab;
        [SerializeField] public float FireRate;
        [SerializeField] public float BulletSpeed;
        
        private float WeaponTimeCounter = 0;
        private float RotationTimeCounter;
        private float ShipAngle;
        private Vector2 PlayerDirection;
        private Vector2 PlayerPosition;
        
        /// <summary>
        /// call functions responsible for moving, aiming and firing  
        /// </summary>
        private void Update()
        {
                Move();
                ShipAngle=Aim();
                if (IsWithinCameraBorders(transform.position,15,10))
                {
                    Fire();
                }
        }
        
        /// <summary>
        /// Fire 4 Bullets Diagonally given certain bullet speed and fire rate 
        /// </summary>
        public void Fire()
        {
            WeaponTimeCounter += Time.deltaTime;
            if (WeaponTimeCounter > FireRate)
            {
                for (int i = 0; i <= 3; i++)
                {
                    var angle = ((i * 90)+ShipAngle)*Mathf.Deg2Rad;
                    var direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                    var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
                }
                WeaponTimeCounter = 0;
            }
        }
        
        /// <summary>
        /// move the ship toward the player position
        /// </summary>
        public void Move()
        {
            var target = Vector3.MoveTowards(transform.position, PlayerPosition, EnemySpeed*Time.deltaTime);
            transform.position = target;
        }
        
        /// <summary>
        /// Rotates the ship around its axis
        /// </summary>
        /// <returns>current ship rotation angle in degrees</returns>
        public float Aim()
        {
            RotationTimeCounter += Time.deltaTime;
            var shipAxis = 2 * RotationTimeCounter * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,shipAxis);
            return shipAxis;
        }
    }
}