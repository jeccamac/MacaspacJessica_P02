using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierKey : MonoBehaviour
{
    [SerializeField] GameObject barrierVolume;
    [SerializeField] AudioSource _soundKey = null;

    public float _rotateSpeed = 0.5f;

    public void Start()
    {
        _soundKey = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(_rotateSpeed, _rotateSpeed, 0, Space.World);
    }
    //public void OnTriggerEnter(Collider other)
    public void UnlockBarrier()
    {
        barrierVolume.SetActive(false);
        _soundKey.Play();
        // particle effect?
        _rotateSpeed = 0f;
        //Destroy(gameObject, 0.5f);
    }
}
