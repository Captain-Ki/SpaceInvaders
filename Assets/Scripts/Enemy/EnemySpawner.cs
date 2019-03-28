using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static List<EnemyBehavior> enemies = new List<EnemyBehavior> { };

    public GameObject enemy;

    public float enemySpawnTime = 1;

    public int numberOfEnemiesPerWave = 30;



    private void Update()
    {
        if (enemies.Count <= numberOfEnemiesPerWave)
        {
            Invoke("SpawnEnemies", enemySpawnTime);
        }
    }

    private void SpawnEnemies()
    {
        CancelInvoke("SpawnEnemies");
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

}
