using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealVolume : MonoBehaviour
{
    public AudioSource sndHeal;
    public float _rotateSpeed = 0.5f;

    GameObject _playerObj;
    PlayerStats _playerHealth;

    public void Start()
    {
        _playerObj = GameObject.Find("FPS Player");
        _playerHealth = _playerObj.GetComponent<PlayerStats>();

        sndHeal = GetComponent<AudioSource>();
    }
    public void Update()
    {
        transform.Rotate(0, _rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("heal box");
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();
        Debug.Log("Heal collision");
        // if we found player, continue
        if (thisPlayer != null)
        {
            _playerHealth.AddHeal(10);
            sndHeal.Play();
            Destroy(gameObject, 0.5f);
        }
    }
   
}
