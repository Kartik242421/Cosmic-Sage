using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float objectDistance = -40f;
    [SerializeField] private float despawnDistance = -110f;
    [SerializeField] private float spawnDelay = 1f; // Adjust as needed

    private bool canSpawnGround = true;
    private float lastSpawnTime;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;

        if (Time.time - lastSpawnTime >= spawnDelay && transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)
        {
            ObjectSpawner.instance.SpawnGround();
            canSpawnGround = false;
            lastSpawnTime = Time.time;
        }

        if (transform.position.z <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);
        }
        else if (transform.position.z > despawnDistance && !canSpawnGround)
        {
            canSpawnGround = true; // Reset canSpawnGround if the object is beyond despawnDistance
        }
    }
}
