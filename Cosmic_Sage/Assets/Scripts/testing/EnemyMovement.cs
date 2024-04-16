using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints defining the path
    private int currentWaypointIndex = 0; // Index of the current waypoint
    public float speed = 5f; // Movement speed of the enemy ship

    void Update()
    {
        // Check if waypoints are set and there are waypoints to follow
        if (waypoints != null && waypoints.Length > 0)
        {
            // Move towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // Check if reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                // Check if reached the last waypoint
                if (currentWaypointIndex == 0)
                {
                    // Destroy the enemy GameObject
                    Destroy(gameObject);
                }
            }

            // Calculate the direction to the next waypoint
            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            // Update the rotation of the enemy to face the direction smoothly
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
            }
        }
        else
        {
            Debug.LogWarning("No waypoints set for the enemy.");
        }
    }
}
