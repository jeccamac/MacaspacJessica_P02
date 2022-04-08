using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierKey_Bridge : MonoBehaviour
{
    [SerializeField] GameObject barrierVolume1;
    [SerializeField] GameObject barrierVolume2;
    [SerializeField] AudioSource _soundKey = null;

    public float _rotateSpeed = 0.5f;

    public void Start()
    {
        barrierVolume1.SetActive(false);
        barrierVolume2.SetActive(false);
        _soundKey = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(_rotateSpeed, _rotateSpeed, 0, Space.World);
    }
    //public void OnTriggerEnter(Collider other)
    public void UnlockShields()
    {
        Debug.Log("Enable Shields");
        barrierVolume1.SetActive(true);
        barrierVolume2.SetActive(true);
        _soundKey.Play();
        // particle effect?
        _rotateSpeed = 0f;
        //Destroy(gameObject, 0.5f);
    }
}
