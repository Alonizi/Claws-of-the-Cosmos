using System;
using UnityEngine;

namespace EnemySystem
{
    public class Ufo:VehicleController,IEnemy
    {
        [SerializeField] private GameObject BulletPrefab;
        [SerializeField] private float FireRate;
        [SerializeField] private float BulletSpeed;
        
        private float WeaponTimeCounter = 0;
        private float RotationTimeCounter; 
        private Vector2 PlayerDirection;
        private Vector2 PlayerPosition;
        
        private void Update()
            {
                Move();
                if (IsWithinCameraBorders(transform.position,15,10))
                {
                    Fire();
                }

            }
        public void Fire()
        {
            WeaponTimeCounter += Time.deltaTime;
            if (WeaponTimeCounter > FireRate)
            {
                for (int i = 0; i <= 3; i++)
                {
                    var axisDegree = 2 * RotationTimeCounter * Mathf.Rad2Deg;
                    var angle = ((i * 90)+axisDegree)*Mathf.Deg2Rad;
                    var direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                    var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
                }
                WeaponTimeCounter = 0;
            }
            
        }
        public void Move()
        {
            RotationTimeCounter += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,2*RotationTimeCounter*Mathf.Rad2Deg);
            var di = Vector3.MoveTowards(transform.position, PlayerPosition, EnemySpeed*Time.deltaTime);
            transform.position = di;
        }
        public float Aim()
        {
            return 0f;
        }

    }
}