using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealVolume : MonoBehaviour
{
    public AudioSource sndHeal;
    public float _rotateSpeed = 0.5f;

    public void Start()
    {
        sndHeal = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(_rotateSpeed, _rotateSpeed, 0, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();
        Debug.Log("Heal collision");
        // if we found player, continue
        if (thisPlayer != null)
        {
            thisPlayer.AddHeal(10);
            sndHeal.Play();
            Destroy(gameObject, 1f);
        }
    }
}
