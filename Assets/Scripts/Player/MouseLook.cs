using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    GameObject levelController;
    Level01Controller level01Control;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // curser invisible
        Cursor.lockState = CursorLockMode.Locked;

        levelController = GameObject.Find("LevelController");
        level01Control = levelController.GetComponent<Level01Controller>();
    }

    // Update is called once per frame
    public void Update()
    {
        var _mouseSensitivity = level01Control.mouseSensitivity;

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // implement mouseY with y axis rotation on camera
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp rotation so doesn't move beyond 90degrees both ways

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // allows us to use clamp ^
        playerBody.Rotate(Vector3.up * mouseX); // implement mouseX with left/right

    }
}
