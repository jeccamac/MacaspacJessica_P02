﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // reference characterController

    public float characterSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck; // reference to object
    public float groundDistance = 0.4f; // radius of sphere for groundCheck
    public LayerMask groundMask; // control what object the sphere should check for

    Vector3 velocity;
    bool isGrounded; // boolean to check if player is on the ground true/false

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // create sphere based on these numbers, and if true/false

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // force player down to ground
            Debug.Log("Player is grounded");
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Vector3 move = new Vector3(x, 0f, z); < these are global coordinates, not specific to player rotation
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * characterSpeed * Time.deltaTime); // motor that drives the player

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump is pressed");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // squareroot of velocity needed to jump
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
