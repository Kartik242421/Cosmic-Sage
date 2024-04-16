using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; 
    private int currentWaypointIndex = 0; 
    public float speed = 5f; 
    public float turnSpeed = 5f; 

    void Update()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                if (currentWaypointIndex == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("No waypoints set for the enemy.");
        }
    }
}
