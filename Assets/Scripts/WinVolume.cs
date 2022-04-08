using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] GameObject winPanel = null;
    [SerializeField] GameObject playerPanel = null;

    private AudioSource sndWin;

    public void Awake()
    {
        winPanel.SetActive(false);
        sndWin = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerStats fpsPlayer = other.gameObject.GetComponent<PlayerStats>();
        PlayerMovement playerMove = other.gameObject.GetComponent<PlayerMovement>();
        // if we found player, continue
        if (fpsPlayer != null)
        {
            //other.gameObject.SetActive(false); //deactivate player
            //Destroy(gameObject, 1f); // destroy this win volume
            sndWin.Play();
            winPanel.SetActive(true);
            playerPanel.SetActive(false);
            playerMove.enabled = false; // turn off movement script
            Cursor.lockState = CursorLockMode.None; // enable cursor
            
        }
    }
}

