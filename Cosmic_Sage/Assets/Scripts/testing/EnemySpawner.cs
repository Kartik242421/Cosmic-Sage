using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; 
    public Transform spawnPoint; 
    public Transform[] waypoints; 
    public float spawnRange = 10f; 
    public float minSpawnInterval = 1f; 
    public float maxSpawnInterval = 3f; 
    public float enemySpeed = 5f; 

    private GameObject player; 
    private float timer = 0f; 
    private float spawnInterval; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (player != null && Vector3.Distance(player.transform.position, spawnPoint.position) <= spawnRange)
        {

            if (timer >= spawnInterval)
            {

                timer = 0f;
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                SpawnRandomEnemy();
            }
        }
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count > 0)
        {
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.waypoints = waypoints;
                enemyMovement.speed = enemySpeed;
            }
            else
            {
                Debug.LogWarning("Enemy prefab does not have the EnemyMovement component.");
            }
        }
        else
        {
            Debug.LogWarning("No enemy prefabs assigned to the enemyPrefabs list.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPoint.position, spawnRange);
    }
}
