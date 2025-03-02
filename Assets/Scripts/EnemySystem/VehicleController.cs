using System;
using System.Collections;
using UnityEngine;

namespace EnemySystem
{
    public class VehicleController:MonoBehaviour
    {
        [SerializeField] protected int Health = 5 ;
        [SerializeField] protected float EnemySpeed = 0.00075f;
        
        protected PlayerMovement Player;
        protected Rigidbody2D RigidComponent;
        protected GameManager GameManager;
        protected AudioManager Audio;

        private void Awake()
        {
            Audio = FindAnyObjectByType<AudioManager>();
            Player = FindAnyObjectByType<PlayerMovement>();
            GameManager = FindAnyObjectByType<GameManager>();
            RigidComponent = GetComponent<Rigidbody2D>();
        }
        
        protected void OnTriggerEnter2D(Collider2D other){
            
            Debug.Log("Generic Collision");
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);
                Health--;
                
                if (Health == 0)
                {
                    //Instantiate(DestroyVfx, transform.position, quaternion.identity).GetComponent<ParticleSystem>().Play();
                    //DestroyVfx.transform.parent = 
                    //DestroyVfx.Play();
                    //DestroySfx.Play();
                    Audio.PlayeSFX(Audio.DestroySFX);
                    Destroy(gameObject,.2f);
                    //StartCoroutine(DisappearShip());
                    //FireVfx.Stop();
                    //DestroyVfx.Play();
                    GameManager.IncreaseScore();
                }
            }
        }
        protected void OnCollisionExit2D(Collision2D other)
        {
            StartCoroutine(ZeroForce());
        }
        
        protected bool IsWithinCameraBorders(Vector2 position , float cameraX , float cameraY)
        {
            return (position.x > -cameraX && position.x < cameraX) && (position.y > -cameraY && position.y < cameraY);
        }
        protected IEnumerator ZeroForce()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
            RigidComponent.totalForce = Vector2.zero;
            RigidComponent.linearVelocity = Vector2.zero;
        }
    }
}