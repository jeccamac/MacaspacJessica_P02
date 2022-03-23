using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardVolume : MonoBehaviour
{
    private AudioSource _soundKill;

    private void Awake()
    {
        //_soundKill = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();
        // if we found player, continue
        if (thisPlayer != null)
        {
            // if player is not invincible
            if (thisPlayer.invincibility == false)
            {
                Debug.Log("You took a hit!");
                // hit player
                thisPlayer.TakeDamage(50);

                //_soundKill.Play();

                //Destroy(gameObject, 1f);
            }
            else { Debug.Log("Invincible!"); }

        }
    }
}
