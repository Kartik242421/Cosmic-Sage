using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // Find all instances of MusicPlayer in the scene
        MusicPlayer[] musicPlayers = FindObjectsOfType<MusicPlayer>();

        // If there is more than one instance, destroy the current one
        if (musicPlayers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // If there is only one instance, don't destroy it when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}
