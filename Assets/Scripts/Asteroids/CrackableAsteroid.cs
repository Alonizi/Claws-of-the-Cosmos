using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrackableAsteroid : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] private GameObject HealthItem; 
    [SerializeField] private Sprite[] CrackingSprites;
    public float AsteroidSpeed =10;
    public float MaxLifeTime = 90f;

    private int Health=5;
    private SpriteRenderer RenderComponent;
    private Rigidbody2D RigidComponent;
    private AudioManager AudioManager;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        AsteroidSpeed = Random.Range(7.5f, 12f);
        RenderComponent=GetComponent<SpriteRenderer>();
        RigidComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetTrajectory(Vector2 direction){
       Debug.LogWarning(direction);
        RigidComponent.AddForce(direction * AsteroidSpeed);
        Destroy(gameObject , MaxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Health--;

            //destroy bullet
            Destroy(other.gameObject);
            
            AudioManager.PlayeSFX(AudioManager.colide);
            PlayParticleEffect(other.gameObject.transform.position);

            switch (Health)
            {
                case 4:
                    RenderComponent.sprite = CrackingSprites[0];
                    break;
                case 2:
                    RenderComponent.sprite = CrackingSprites[1];
                    break;
            }

            if (Health <= 0)
            {
                Instantiate(HealthItem, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Enemy_Bullet")
        {
            Destroy(other.gameObject);
        }
    }
    
    private void PlayParticleEffect(Vector3 position)
    {
        if (hitEffect != null)
        {
            ParticleSystem effect = Instantiate(hitEffect, position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
    }
}
