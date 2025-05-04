//copyrights Abdulaziz Alonizi 2025

using System.Collections.Generic;
using System.Linq;
using EnemySystem;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Responsible for Spawning enemies on each level 
/// in a rogue like fashion (increasing difficulty with each level) 
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int Level;
    [SerializeField] private int LastLevel = 10;
    [SerializeField] private VehicleController FighterJetEnemy;
    [SerializeField] private VehicleController UfoEnemy;
    
    [Header("Rogue Modifiers for Fighter-jet  ")]
    [SerializeField] private int RogueModifier_Enemy1_SpawnLevel = 1;
    [SerializeField] private int RogueModifier_Enemy1_SpawnEndLevel = 6;
    [SerializeField] private float RogueModifier_Enemy1_Speed = 1;
    [SerializeField] private float RogueModifier_Enemy1_BulletRate =1;
    [SerializeField] private float RogueModifier_Enemy1_BulletSpeed = 1;
    
    [Header("Rogue Modifiers for UFOs ")]
    [SerializeField] private int RogueModifier_EnemyFan_SpawnLevel = 3;
    [SerializeField] private int RogueModifier_EnemyFan_SpawnEndLevel = 8;
    [SerializeField] private float RogueModifier_EnemyFan_Speed = 1;
    [SerializeField] private float RogueModifier_EnemyFan_BulletRate = 1;
    [SerializeField] private float RogueModifier_EnemyFan_BulletSpeed = 1;
    
    private GameManager Manager; 
    private List<VehicleController> EnemiesAlive; 
    private float GameTime;

    /// <summary>
    /// Cache in GameManager object available in scene 
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameTime = 0;
        Manager = FindAnyObjectByType<GameManager>();
    }
    /// <summary>
    /// responsible for keeping track of levels, enemies count
    /// and start new levels accordingly by calling the RogueLike() Function
    /// </summary>
    private void Update()
    {
        GameTime += Time.deltaTime;
        EnemiesAlive = FindObjectsByType<VehicleController>(0).ToList();
        // check if player killed all enemies 
        if (EnemiesAlive.Count <= 0)
        {   // start new level, if last level not reached
            if (Level < LastLevel+1)
            {
                Level++;
                Manager.UpdateWave(Level);
                RogueLike();
            }// end game if last level  
            else if (Level == LastLevel + 1)
            {
                Manager.Win();
            }
        }
        Debug.Log($"Enemies Alive {EnemiesAlive.Count}");
        Debug.Log($"Level{Level}");
    }
    /// <summary>
    /// spawn enemies based on current level and provided 'Rogue-Like' modifiers
    /// </summary>
    private void RogueLike()
    {
        // start spawning FighterJets
        for (int i = RogueModifier_Enemy1_SpawnLevel; i <= Level && Level <= RogueModifier_Enemy1_SpawnEndLevel; i++)
        {
            var randomLocation = RandomLocation();
            FighterJet fighterJet =(FighterJet) Instantiate(FighterJetEnemy, randomLocation, Quaternion.identity);
            fighterJet.BulletSpeed = Level * RogueModifier_Enemy1_BulletSpeed;
            fighterJet.FireRate = (1f/Level) * RogueModifier_Enemy1_BulletRate;
            fighterJet.EnemySpeed = Level * RogueModifier_Enemy1_Speed; 
        }
        
        // start spawning UFOs
        for (int i = RogueModifier_EnemyFan_SpawnLevel; i <= Level && Level <= RogueModifier_EnemyFan_SpawnEndLevel; i++)
        {
            var randomLocation = RandomLocation();
            Ufo ufo = (Ufo) Instantiate(UfoEnemy, randomLocation, Quaternion.identity);
            ufo.BulletSpeed = Level * RogueModifier_EnemyFan_BulletSpeed;
            ufo.FireRate = 1f/Level * RogueModifier_EnemyFan_BulletRate;
            ufo.EnemySpeed = Level * RogueModifier_EnemyFan_Speed; 
        }
    }
    /// <summary>
    /// Generate random location within outside camera borders
    /// </summary>
    /// <returns></returns>
    private Vector2 RandomLocation()
    {
        Vector2 randomLocation; 
        randomLocation.x = Random.Range(-30,25);
        if (randomLocation.x >= -18 && randomLocation.x <= 18)
        {
            randomLocation.y = Random.Range(-15,-12);
            randomLocation.y *= Random.Range(0, 2) * 2 - 1;//multiply by 1 or -1
        }
        else
        {
            randomLocation.y = Random.Range(5, 10);
        }

        return randomLocation;
    }
    /// <summary>
    /// destroy all enemies in scene ( for testing ) 
    /// </summary>
    private void DestroyAllEnemies()
    {
        foreach (var enemy in EnemiesAlive)
        {
            Destroy(enemy.gameObject);
        }
    }
}
