using System;
using UnityEngine;

namespace EnemySystem
{
    public class Kamikaze : VehicleController,IEnemy
    {
        private Vector3 InitialPlayerPosition;
        private Vector3 InitialPlayerDirection;

        private void Start()
        {
            InitialPlayerPosition = Player.transform.position;
            InitialPlayerDirection =  InitialPlayerPosition - transform.position;
        }

        private void Update()
        {
            Aim();
            Move();
        }
        public float Aim()
        {
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(InitialPlayerDirection.y, InitialPlayerDirection.x)) - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return angle;
        }
        public void Move()
        {
            RigidComponent.linearVelocity = (Vector2)InitialPlayerDirection.normalized*EnemySpeed;
        }
        public void Fire()
        {
            
        }
        
    }
}