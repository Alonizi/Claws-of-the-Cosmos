// copyrights Abdulaziz Alonizi 2025

using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// FighterJet Script for an enemy ship that follows and shoots the player ship
    /// </summary>
    public class FighterJet : VehicleController, IVehicle,IWeapon
    {
        [SerializeField] public float FireRate;
        [SerializeField] public float BulletSpeed;
        [SerializeField] public float ShootingRange = 10;
        [SerializeField] private GameObject BulletPrefab;
        
        private float TimeCounter = 0;
        private float ShipAngle ;
        private Vector2 PlayerDirection;
        private Vector2 PlayerPosition;
        
        /// <summary>
        /// keep updated data on player's position and direction.
        /// call function responsible for moving, aiming and firing  
        /// </summary>
        private void Update()
        {
            PlayerPosition = Player.transform.position;
            PlayerDirection =  PlayerPosition - (Vector2)transform.position;
            Move();
            ShipAngle = Aim();
            if (IsWithinCameraBorders(transform.position,15,10) && IsNearPlayer(ShootingRange))
            {
                Fire();
            }
        }
        /// <summary>
        /// Fire Weapon given certain bullet speed and fire rate 
        /// </summary>
        public void Fire()
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > FireRate)
            {
                var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, ShipAngle));
                bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * PlayerDirection.normalized;
                TimeCounter = 0; 
            }
        }
        /// <summary>
        /// Aim the enemy ship toward the player direction 
        /// </summary>
        /// <returns>the current rotation angle of the ship in degrees</returns>
        public float Aim()
        {
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(PlayerDirection.y, PlayerDirection.x)) - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return angle;
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
        /// check to see if the enemy is within the boundaries of the player's ship 
        /// </summary>
        /// <param name="distance">allowed distance between the enemy and the player ship</param>
        /// <returns></returns>
        private bool IsNearPlayer(float distance)
        {
            return PlayerDirection.magnitude <= distance; 
        }
    }
}