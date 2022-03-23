using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    GameObject lvlController;
    Level01Controller scoreTracker;

    public void Start()
    {
        lvlController = GameObject.Find("LevelController");
        scoreTracker = lvlController.GetComponent<Level01Controller>();
    }
    public void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();
            
        // if we found player, continue
        if (thisPlayer != null)
        {
            scoreTracker.IncreaseScore(10);
            Destroy(gameObject, 1f);
        }
    }
}
