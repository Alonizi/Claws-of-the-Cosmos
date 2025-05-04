//copyrights Abdulaziz Alonizi 
namespace EnemySystem
{
    /// <summary>
    /// Interface responsible for abstracting basic movement and aiming of 
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Abstract Movement Mechanic
        /// </summary>
        public void Move();
        /// <summary>
        /// Abstract Aim Mechanic
        /// </summary>
        /// <returns></returns>
        public float Aim();
        
    }
}