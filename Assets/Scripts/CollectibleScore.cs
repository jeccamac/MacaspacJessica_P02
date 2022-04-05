using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    GameObject lvlController;
    Level01Controller scoreTracker;

    [SerializeField] AudioSource _soundCollect = null;

    public void Start()
    {
        lvlController = GameObject.Find("LevelController");
        scoreTracker = lvlController.GetComponent<Level01Controller>();
        _soundCollect = GetComponent<AudioSource>();
    }
    /*
    public void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();
            
        // if we found player, continue
        if (thisPlayer != null)
        {
            scoreTracker.IncreaseScore(10);
            _soundCollect.Play();
            Destroy(gameObject, 1f);
        }
    }
    */
}
