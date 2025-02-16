using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player movment 
    [Header("Ship Parameters")]
 
    [SerializeField] float shipAcceleration = 2f;
    [SerializeField] float shipMaxAcceleration = 5f;
    [SerializeField] float turnSpeed=1;
     float shipRotationDirection;
    [SerializeField] ParticleSystem Fire ; 
    Animator _animator;
   
    Rigidbody2D shipRigidbody;
    bool isAlive ;
    bool isAccelerating= false;
    bool isAcceleratingDown = false ;

    [Header ("Bullet")]
    [SerializeField] Bullet bulletPrefab;

    [Header("Jet")]
    [SerializeField] ParticleSystem jetPrefab;
    [SerializeField] ParticleSystem JetLeft;
    [SerializeField] ParticleSystem JetRight;


    GameManager gameManager;
    AudioManager audioManager; 

    [System.Obsolete]
    private void Start() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        shipRigidbody = GetComponent<Rigidbody2D>();
        isAlive = true;
        gameManager = FindObjectOfType<GameManager>();
        jetPrefab.Stop();
        JetLeft.Stop();
        JetRight.Stop();
        _animator = GetComponent<Animator>();
        
    }

    private void Update() {
        // so only workes when alive 
        if (isAlive ){
        HandelShipAcceleration();
        HandelRotation();
        
        
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
        
            Shoot();
            //_animator.ResetTrigger("Idel");
            _animator.SetBool("IsShoot", true);
        }else{
           // _animator.ResetTrigger("Shoot");
            _animator.SetBool("IsShoot", false);
        }
  
    }
   

    private void FixedUpdate() {

     if (isAlive && isAccelerating) {

        shipRigidbody.AddForce(shipAcceleration * this.transform.up);
        shipRigidbody.linearVelocity = Vector2.ClampMagnitude(shipRigidbody.linearVelocity, shipMaxAcceleration);

        // to stop the ship we use clampMagnitude 

        }
    if(shipRotationDirection != 0f){
        shipRigidbody.AddTorque(shipRotationDirection * turnSpeed);
        }
        
    }

    public void HandelShipAcceleration(){
       if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
        isAccelerating = true;
        jetPrefab.Play();

      PlaySound(); 
    
        
    }else{
        jetPrefab.Stop();
        isAccelerating = false;
        RestSound1();
    }
    }

    private void PlaySound(){
        audioManager.PlayOnce(audioManager.AirPush);
    }
    private void RestSound1(){
        audioManager.ResetPlayOnce();
    }
    public void HandelRotation(){
        if( Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow) ){
            shipRotationDirection = -0.5f;
            JetRight.Stop();
            JetLeft.Play();   
            
        }
        else if ( Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow) ){
            shipRotationDirection = 0.5f;
            JetLeft.Stop(); 
            JetRight.Play();
             
           
        }
        else {
            shipRotationDirection =0;
            JetLeft.Stop();
            JetRight.Stop();
           
        }

    }

    private void Shoot(){
        audioManager.PlayeSFX(audioManager.Fire);
        Fire.Play();
        //var fire = Instantiate (Fire ,transform.GetChild(0).position , Quaternion.identity );
        Bullet bullet = Instantiate(this.bulletPrefab, transform.GetChild(0).position, this.transform.rotation );
        bullet.Project(this.transform.up);
    }
      private void OnCollisionEnter2D(Collision2D other)
      {
          // if (other.gameObject.tag == "Astroids" || other.gameObject.tag == "Enemy")
          // {
          //     gameManager.PlayerTakeDamage(10);
          //
          //
          // }
          if(other.gameObject.tag == "Enemy_Bullet"){
            
            audioManager.PlayeSFX(audioManager.ShipHit);
          }
     }

     public void Die(){
        Destroy(gameObject);
     }


}

