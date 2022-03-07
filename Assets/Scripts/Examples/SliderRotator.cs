using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderRotator : MonoBehaviour
{
    public float rotateSpeed = 3f;

    [SerializeField] Vector3 rotateDirection = new Vector3(0, 1, 0); // x, y, z axis

    private void Update()
    {
        // rotate the object
        transform.Rotate(rotateDirection * rotateSpeed);
    }
}
