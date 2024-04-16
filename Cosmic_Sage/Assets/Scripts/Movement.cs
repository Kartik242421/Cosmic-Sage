using UnityEngine;
using UnityEngine.Playables;

public class Movement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public PlayableDirector playerTimeline;
    private int currentWaypointIndex = 0;

    private bool isTimelinePlayed = false;

    void Start()
    {
        if (playerTimeline != null)
        {
            playerTimeline.Play();
            playerTimeline.stopped += OnPlayerTimelineStopped;
        }
        else
        {
            enabled = true;
            isTimelinePlayed = true;
        }
    }

    void Update()
    {
        if (isTimelinePlayed && enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }

            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void OnPlayerTimelineStopped(PlayableDirector director)
    {
        enabled = true;
        isTimelinePlayed = true;
    }
}
