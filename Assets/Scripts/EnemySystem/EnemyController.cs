using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private GameObject DestroyVfx;
    [SerializeField] private AudioSource DestroySfx;
    [SerializeField] private AudioSource HitSfx;
    [SerializeField] private ParticleSystem HitVfx; 
    [SerializeField] private ParticleSystem FireVfx; 
    private GameManager GameManager; 
    public float Health = 15; 
    public int EnemyType = 1; 
    public float EnemySpeed = 0.00075f;
    public float ShootingRange = 10;
    private PlayerMovement Player;
    private EnemyWeaponController Weapons;
    private Vector3 PlayerPosition;
    private Vector3 PlayerDirection;
    private float EnemiesTimeCounter;
    private SpriteRenderer ShipRenderer;
    private Vector3 InitialPlayerPosition;
    private Vector3 InitialPlayerDirection;
    private AudioManager Audio;
    private Rigidbody2D RigidComponent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Audio = FindAnyObjectByType<AudioManager>();
        ShipRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //SpawnPosition = new Vector3(-11f, 6, 0f);
        Player = FindAnyObjectByType<PlayerMovement>();
        Weapons = GetComponent<EnemyWeaponController>();
        EnemiesTimeCounter = 0;
        GameManager = FindAnyObjectByType<GameManager>();
        InitialPlayerPosition = Player.transform.position;
        InitialPlayerDirection =  PlayerPosition - transform.position;
        RigidComponent = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void Update()
    {
         //keep track of players positional data 
         PlayerPosition = Player.transform.position;
         PlayerDirection =  PlayerPosition - transform.position;
         switch (EnemyType)
         {
             case 1:
                 Type1(); 
                 break;
             case 2 :
                 Type2();
                 break;
             case 3 :
                 Type3();
                 break;
         }
    }
    //Enemy 1 
    private void Type1()
    {
        var di = Vector3.MoveTowards(transform.position, PlayerPosition, EnemySpeed*Time.deltaTime);
        transform.position = di;
        var axis = Weapons.Aim(PlayerDirection);
        if (IsWithinCameraBorders(transform.position,15,10) && IsNearPlayer(ShootingRange))
        {
            Weapons.AutoFire(PlayerDirection, axis);
        }
    }
    //Enemy_Fan
    private void Type2()
    {
        EnemiesTimeCounter += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,0,2*EnemiesTimeCounter*Mathf.Rad2Deg);
        var di = Vector3.MoveTowards(transform.position, PlayerPosition, EnemySpeed*Time.deltaTime);
        transform.position = di;
        if (IsWithinCameraBorders(transform.position,15,10))
        {
            Weapons.FireDiagonally(axisDegree: 2 * EnemiesTimeCounter * Mathf.Rad2Deg);
        }
    }
    //Kamikazze
    private void Type3()
    {
        var di = Vector3.MoveTowards(transform.position, InitialPlayerPosition, EnemySpeed*Time.deltaTime);
        transform.position = di;
        var axis = Weapons.Aim(InitialPlayerDirection);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Bullet")
        {
            
            Destroy(other.gameObject);
            Health--;
            HitVfx.transform.position = other.transform.position;
            HitVfx.Play();
            HitSfx.Play();

            if (Health <= 5 && (EnemyType==1))
            {
                FireVfx.Play() ;
            }
            if (Health == 0)
            {
                Instantiate(DestroyVfx, transform.position, quaternion.identity).GetComponent<ParticleSystem>().Play();
                DestroySfx.Play();
                Audio.PlayeSFX(Audio.DestroySFX);
                Destroy(gameObject,.2f);
                FireVfx.Stop();
                GameManager.IncreaseScore();
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        StartCoroutine(ZeroForce());
    }

    private bool IsWithinCameraBorders(Vector2 position , float cameraX , float cameraY)
    {
        return (position.x > -cameraX && position.x < cameraX) && (position.y > -cameraY && position.y < cameraY);
    }

    private bool IsNearPlayer(float distance)
    {
        return PlayerDirection.magnitude <= distance; 
    }

    /// <summary>
    /// Reset Force and Velocity
    /// </summary>
    /// <returns></returns>
    private IEnumerator ZeroForce()
    {
        yield return new WaitForSeconds(Random.Range(2, 6));
        RigidComponent.totalForce = Vector2.zero;
        RigidComponent.linearVelocity = Vector2.zero;
    }
    
    
}
