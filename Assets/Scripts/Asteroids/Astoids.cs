
using UnityEngine;
using UnityEngine.UIElements;

public class Astoids : MonoBehaviour
{
    [Header("Sprite Mix")]
    public Sprite[] sprites;
    public float size = 1f ; 
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    //public GameManager gameManager;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    public float astroidsSpeed =10;
    public float maxLifeTime = 30f;
    [SerializeField] ParticleSystem hitEffect;
    AudioManager audioManager;

    GameManager gameManager;


    
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
            if (!GetComponent<PolygonCollider2D>())
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
        //to pick random sprite
        _spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        this.transform.eulerAngles = new Vector3(0,0,Random.value * 360);

        transform.localScale =  size * Vector3.one;
        _rigidbody2D.mass = size ;
/*
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direcotion = new Vector2(Random.value , Random.value).normalized;
        // so it rindomize the speed based on size 
        float spawnSpeed = Random.Range(2f-size , 3f - size);
        rb.AddForce(direcotion * spawnSpeed, ForceMode2D.Impulse);
        //register creation 
        
        gameManager.asteroidCount++;
        */
    }

    public void SetTrajectory(Vector2 direction){
       
        _rigidbody2D.AddForce(direction * astroidsSpeed);
    Destroy(gameObject , maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player" || other.gameObject.tag =="Enemy"){
            
            audioManager.PlayeSFX(audioManager.colide);
            PlayParticleEffect();

            if(size  > minSize) {
                Split();
                Split();
            }
            Destroy(gameObject);
            gameManager.IncreaseScore();
        }
          if(other.gameObject.tag == "Bullet" ){
            Destroy(other.gameObject);
          }
    }

    private void Split(){

        //to change astroid posiotion 
        Vector2 pos = transform.position;
        pos +=Random.insideUnitCircle *1f;
        Astoids half = Instantiate(this , pos,transform.rotation);
        half.size = size * 0.5f ;
        ResizeColliders(half);
        half.SetTrajectory(Random.insideUnitCircle.normalized *astroidsSpeed);

    }
 public void ResizeColliders(Astoids asteroid)
{
 
    BoxCollider2D boxCollider = asteroid.GetComponent<BoxCollider2D>();
    if (boxCollider != null)
    {
        boxCollider.size *= 0.5f;
    }


}
       private void PlayParticleEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
    }
}
