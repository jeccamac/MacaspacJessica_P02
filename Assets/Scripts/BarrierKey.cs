using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierKey : MonoBehaviour
{
    [SerializeField] GameObject barrierVolume;

    public float _rotateSpeed = 0.5f;

    private void Update()
    {
        transform.Rotate(_rotateSpeed, _rotateSpeed, 0, Space.World);
    }
    public void OnTriggerEnter(Collider other)
    {
        barrierVolume.SetActive(false);
        // particle effect?
        Destroy(gameObject, 0.5f);
    }
}
