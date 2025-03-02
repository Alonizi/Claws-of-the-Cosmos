using System;
using UnityEngine;

namespace EnemySystem
{
    public class FighterJet : VehicleController, IEnemy
    {
        [SerializeField] public float FireRate;
        [SerializeField] public float BulletSpeed;
        [SerializeField] public float ShootingRange = 10;
        [SerializeField] private GameObject BulletPrefab;
        
        private float TimeCounter = 0;
        private float Axis ;
        private Vector2 PlayerDirection;
        private Vector2 PlayerPosition;
        

        private void Update()
        {
            PlayerPosition = Player.transform.position;
            PlayerDirection =  PlayerPosition - (Vector2)transform.position;
            Move();
            Axis = Aim();
            if (IsWithinCameraBorders(transform.position,15,10) && IsNearPlayer(ShootingRange))
            {
                Fire();
            }
        }

        public void Fire()
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > FireRate)
            {
                var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, Axis));
                bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * PlayerDirection.normalized;
                TimeCounter = 0; 
            }
        }

        public float Aim()
        {
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(PlayerDirection.y, PlayerDirection.x)) - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return angle;
        }

        public void Move()
        {
            var di = Vector3.MoveTowards(transform.position, PlayerPosition, EnemySpeed*Time.deltaTime);
            transform.position = di;
        }
        
        private bool IsNearPlayer(float distance)
        {
            return PlayerDirection.magnitude <= distance; 
        }
    }
}