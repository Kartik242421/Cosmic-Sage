using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnGround()
    {
        ObjectPooling.instance.SpawnFromPool("ground", new Vector3(0, 0, 60f), Quaternion.identity);
    }
}
