// Copyrights Abdulaziz Alonizi 2025
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Base (Parent) class to encapsulate common functions,behaviors and properties
    /// between all enemies(child) classes 
    /// </summary>
    public class VehicleController:MonoBehaviour
    {
        [SerializeField] private GameObject DestroyVfx;
        [SerializeField] private AudioSource DestroySfx;
        [SerializeField] public int Health = 5 ;
        [SerializeField] public float EnemySpeed = 0.00075f;
        
        protected PlayerMovement Player;
        protected Rigidbody2D RigidComponent;
        protected GameManager GameManager;
        protected AudioManager Audio;
        
        /// <summary>
        /// Cache necessary components ; 
        /// </summary>
        private void Awake()
        {
            Audio = FindAnyObjectByType<AudioManager>();
            Player = FindAnyObjectByType<PlayerMovement>();
            GameManager = FindAnyObjectByType<GameManager>();
            RigidComponent = GetComponent<Rigidbody2D>();
        }
        /// <summary>
        /// destroy the object and enable Vfx/sfx on trigger event
        /// </summary>
        /// <param name="other"></param>
        protected void OnTriggerEnter2D(Collider2D other){
            Debug.Log("Base Vehicle Collision");
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);
                Health--;
                
                if (Health <= 0)
                {
                    Instantiate(DestroyVfx, transform.position, quaternion.identity).GetComponent<ParticleSystem>().Play();
                    DestroySfx.Play();
                    Audio.PlayeSFX(Audio.DestroySFX);
                    Destroy(gameObject,.2f);
                    GameManager.IncreaseScore();
                }
            }
        }
        /// <summary>
        /// Reset Force and Velocity when Collision has ended
        /// </summary>
        /// <param name="other"></param>
        protected void OnCollisionExit2D(Collision2D other)
        {
            StartCoroutine(ZeroForce());
        }
        /// <summary>
        /// check to see if the object is within camera borders
        /// </summary>
        /// <param name="position"> object position</param>
        /// <param name="cameraX"> camera x axis </param>
        /// <param name="cameraY"> camera y axis </param>
        /// <returns></returns>
        protected bool IsWithinCameraBorders(Vector2 position , float cameraX , float cameraY)
        {
            return (position.x > -cameraX && position.x < cameraX) && (position.y > -cameraY && position.y < cameraY);
        }
        /// <summary>
        /// reset Velocity and Force of an object after certain random time 
        /// </summary>
        /// <returns></returns>
        protected IEnumerator ZeroForce()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
            RigidComponent.totalForce = Vector2.zero;
            RigidComponent.linearVelocity = Vector2.zero;
        }
    }
}