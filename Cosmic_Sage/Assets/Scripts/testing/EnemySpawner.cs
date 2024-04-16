using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to choose from
    public Transform spawnPoint; // The spawn point where enemies will spawn
    public Transform[] waypoints; // Waypoints defining the path for the enemies
    public float spawnRange = 10f; // The range within which the player triggers enemy spawning
    public float minSpawnInterval = 1f; // Lower bound of the spawn interval range
    public float maxSpawnInterval = 3f; // Upper bound of the spawn interval range
    public float enemySpeed = 5f; // Movement speed of the enemy ship

    private GameObject player; // Reference to the player GameObject
    private float timer = 0f; // Timer to track spawn intervals
    private float spawnInterval; // The random spawn interval

    void Start()
    {
        // Find the player GameObject using a tag (you can also assign this reference in the Inspector)
        player = GameObject.FindGameObjectWithTag("Player");

        // Initialize the spawn interval to a random value within the specified range
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the player is within the specified range of the spawn point
        if (player != null && Vector3.Distance(player.transform.position, spawnPoint.position) <= spawnRange)
        {
            // Check if it's time to spawn a new enemy
            if (timer >= spawnInterval)
            {
                // Reset the timer
                timer = 0f;

                // Update the spawn interval to a new random value within the specified range
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

                // Spawn a random enemy from the list
                SpawnRandomEnemy();
            }
        }
    }

    void SpawnRandomEnemy()
    {
        // Check if the enemy prefabs list is not empty
        if (enemyPrefabs.Count > 0)
        {
            // Select a random enemy prefab from the list
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Instantiate the selected enemy prefab at the spawn point position
            GameObject enemy = Instantiate(randomEnemyPrefab, spawnPoint.position, Quaternion.identity);

            // Pass the waypoints and speed to the spawned enemy
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
        // Draw a wire sphere gizmo to visualize the spawn range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPoint.position, spawnRange);
    }
}
