using UnityEngine;

namespace EnemySystem
{
    public interface IEnemy
    {
        public void Move();

        public float Aim();

        public void Fire();
    }
}