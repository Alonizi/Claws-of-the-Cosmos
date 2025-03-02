using System;
using System.Collections.Generic;
using System.Linq;
using EnemySystem;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int Level;
    [SerializeField] private int LastLevel = 10;
    [SerializeField] private VehicleController FighterJetEnemy;
    [SerializeField] private VehicleController UfoEnemy;

    
    
    [Header("Rogue Modifiers for Enemy_1 ")]
    [SerializeField] private int RogueModifier_Enemy1_SpawnLevel = 1;
    [SerializeField] private int RogueModifier_Enemy1_SpawnEndLevel = 6;
    [SerializeField] private float RogueModifier_Enemy1_Speed = 1;
    [SerializeField] private float RogueModifier_Enemy1_BulletRate =1;
    [SerializeField] private float RogueModifier_Enemy1_BulletSpeed = 1;
    
    [Header("Rogue Modifiers for Enemy_Fan ")]
    [SerializeField] private int RogueModifier_EnemyFan_SpawnLevel = 3;
    [SerializeField] private int RogueModifier_EnemyFan_SpawnEndLevel = 8;
    [SerializeField] private float RogueModifier_EnemyFan_Speed = 1;
    [SerializeField] private float RogueModifier_EnemyFan_BulletRate = 1;
    [SerializeField] private float RogueModifier_EnemyFan_BulletSpeed = 1;
    
    private GameManager Manager; 
    private List<VehicleController> EnemiesAlive; 
    private float GameTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameTime = 0;
        Manager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        EnemiesAlive = FindObjectsByType<VehicleController>(0).ToList();
        if (EnemiesAlive.Count <= 0)
        {
            if (Level < LastLevel+1)
            {
                Level++;
                Manager.UpdateWave(Level);
                RogueLike();
            }
            else if (Level == LastLevel + 1)
            {
                Manager.Win();
            }
        }
        Debug.Log($"Enemies Alive {EnemiesAlive.Count}");
        Debug.Log($"Level{Level}");
    }

    private void RogueLike()
    {
        // start spawning Enemy-1 
        for (int i = RogueModifier_Enemy1_SpawnLevel; i <= Level && Level <= RogueModifier_Enemy1_SpawnEndLevel; i++)
        {
            
            int yAxis;
            int xAxis = Random.Range(-30,25);
            if (xAxis > -18 && xAxis < 18)
            {
                yAxis = Random.Range(-12,-10);
                yAxis *= Random.Range(0, 2) * 2 - 1;
            }
            else
            {
                yAxis = Random.Range(5, 10);
            }
            
            FighterJet fighterJet =(FighterJet) Instantiate(FighterJetEnemy, new Vector2(xAxis, yAxis), Quaternion.identity);
            fighterJet.BulletSpeed = Level * RogueModifier_Enemy1_BulletSpeed;
            fighterJet.FireRate = (1f/Level) * RogueModifier_Enemy1_BulletRate;
            fighterJet.EnemySpeed = Level * RogueModifier_Enemy1_Speed; 
        }
        
        // start spawning Enemy-Fan
        for (int i = RogueModifier_EnemyFan_SpawnLevel; i <= Level && Level <= RogueModifier_EnemyFan_SpawnEndLevel; i++)
        {
            int yAxis;
            int xAxis = Random.Range(-30,25);
            if (xAxis >= -18 && xAxis <= 18)
            {
                yAxis = Random.Range(-15,-12);
                yAxis *= Random.Range(0, 2) * 2 - 1;
            }
            else
            {
                yAxis = Random.Range(5, 10);
            }
            Ufo ufo = (Ufo) Instantiate(UfoEnemy, new Vector2(xAxis, yAxis), Quaternion.identity);
            ufo.BulletSpeed = Level * RogueModifier_EnemyFan_BulletSpeed;
            ufo.FireRate = 1f/Level * RogueModifier_EnemyFan_BulletRate;
            ufo.EnemySpeed = Level * RogueModifier_EnemyFan_Speed; 
        }
    }
    
    public void DestroyAllEnemies()
    {
        foreach (var enemy in EnemiesAlive)
        {
            Destroy(enemy.gameObject);
        }
    }
    
}
