using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Interface responsible for abstracting basic Weapon and shooting Mechanic
    /// </summary>

    public interface IWeapon
    { 
        /// <summary>
        /// Abstract Weapon Shooting Mechanic 
        /// </summary>
        public void Fire();
    }
}