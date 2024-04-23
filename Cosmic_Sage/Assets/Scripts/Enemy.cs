using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    GameObject parentGameObject;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

  

    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRunTime");
        AddRigidGBody();
    }

    void AddRigidGBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log($"{name}I'm hit! by {other.gameObject.name}");

        ProcessHit();

        if (hitPoints < 1)
        {
            KillEnemy();
        }

    }
    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        hitPoints--;
        
    }
    void KillEnemy()
    {
        // Increase score
        if (UIManager.Instance != null)
        {
            UIManager.Instance.IncreaseScore(scorePerHit);
        }

        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }


}
