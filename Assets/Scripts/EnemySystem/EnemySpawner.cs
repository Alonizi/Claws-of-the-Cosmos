using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private List<EnemyController> EnemiesAlive; 
    private float GameTime;
    public int Level; 
    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private GameObject Enemy3;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Level = 1; 
        GameTime = 0; 
        //Level1();
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        EnemiesAlive = FindObjectsByType<EnemyController>(0).ToList();
        if (EnemiesAlive.Count <= 0)
        {
            Level++;
            RogueLike();
            
        }
        Debug.Log($"Enemies Alive {EnemiesAlive.Count}");
        Debug.Log($"Level{Level}");

    }
    /// <summary>
    /// Instantiate two enemies of different types
    /// </summary>
    public void Level1()
    {
        Instantiate(Enemy1, new Vector2(15, 0), Quaternion.identity);
        //Instantiate(Enemy2, new Vector2(-15, 0), Quaternion.identity);

    }
    /// <summary>
    /// Instantiate
    /// </summary>
    public void Level2()
    {
        var enemy1 =Instantiate(Enemy1, new Vector2(15, 0), Quaternion.identity);
        var enemy2 = Instantiate(Enemy2, new Vector2(-15, 0), Quaternion.identity);
        enemy1.GetComponent<EnemyWeaponController>().BulletSpeed = 1;
        enemy2.GetComponent<EnemyWeaponController>().FireRate = 0.1f;
    }

    private void RogueLike()
    {
        for (int i = 0; i < Level && Level <= 7; i++)
        {
            int yAxis;
            int xAxis = Random.Range(-12, 12);
            if (xAxis > -10 && xAxis < 10)
            {
                yAxis = Random.Range(-6,-8);
            }
            else
            {
                yAxis = Random.Range(-7, 7);
            }
            var enemy = Instantiate(Enemy1, new Vector2(xAxis, yAxis), Quaternion.identity);
            var enemyWeaponController = enemy.GetComponent<EnemyWeaponController>();
            var enemyController = enemy.GetComponent<EnemyController>();
            enemyWeaponController.BulletSpeed += Level * .75f;
            enemyWeaponController.FireRate += Level * .75f;
        }

        for (int i = 3; i < Level && Level <=10; i++)
        {
            int yAxis;
            int xAxis = Random.Range(-12, 12);
            if (xAxis > -10 && xAxis < 10)
            {
                yAxis = Random.Range(-6,-8);
            }
            else
            {
                yAxis = Random.Range(-7, 7);
            }
            
            Instantiate(Enemy2, new Vector2(xAxis, yAxis), Quaternion.identity);
            var enemy = Instantiate(Enemy1, new Vector2(xAxis, yAxis), Quaternion.identity);
            var enemyWeaponController = enemy.GetComponent<EnemyWeaponController>();
            enemyWeaponController.BulletSpeed += Level * .75f;
            enemyWeaponController.FireRate += Level * .75f;
            
        }
    }
    
}
