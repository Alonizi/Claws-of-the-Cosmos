// copyrights Abdulaziz Alonizi 2025

using System;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Kamikaze Script for an enemy ship that fly toward player's initial position at spawn 
    /// Damages Player Health Heavily on Collision
    /// </summary>
    public class Kamikaze : VehicleController,IVehicle
    {
        private Vector3 InitialPlayerPosition;
        private Vector3 InitialPlayerDirection;
        /// <summary>
        /// get Initial Player position and direction on spawn of the object
        /// </summary>
        private void Start()
        {
            InitialPlayerPosition = Player.transform.position;
            InitialPlayerDirection =  InitialPlayerPosition - transform.position;
        }
        /// <summary>
        /// call functions responsible for moving and aiming   
        /// </summary>
        private void Update()
        {
            Aim();
            Move();
        }
        /// <summary>
        /// aim ship toward initial player direction
        /// </summary>
        /// <returns></returns>
        public float Aim()
        {
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(InitialPlayerDirection.y, InitialPlayerDirection.x)) - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return angle;
        }
        /// <summary>
        /// move ship toward initial player position
        /// </summary>
        public void Move()
        {
            RigidComponent.linearVelocity = (Vector2)InitialPlayerDirection.normalized*EnemySpeed;
        }

        /// <summary>
        /// Explode and Damage Player's Ship On Collision ,
        /// </summary>
        public void OnCollisionEnter2D(Collision2D other)
        {
            throw new NotImplementedException();
        }
    }
}