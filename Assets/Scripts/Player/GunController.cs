using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [Tooltip("The start position of the ray to fire, obj projectile start")]
    [SerializeField] Transform rayOrigin;

    [Tooltip("How far to shoot")]
    [SerializeField] float shootDistance = 10f;

    [Tooltip("Visual feedback when firing, can be light or particles")]
    [SerializeField] GameObject projectileVisualFeedback;

    [SerializeField] int weaponDamage = 20;

    [Tooltip("What objects in the layer you are able to hit")]
    [SerializeField] LayerMask hitLayers;

    RaycastHit objectHit; // stores info about our RaycastHit

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    // fire the weapon using Raycast
    void Shoot()
    {
        // calculate direction to shoot the Ray
        Vector3 rayDirection = playerCamera.transform.forward;
        // cast a Debug Ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f); // start position, direction & distance, color, how long visual before disappearing
        // fire the Raycast
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers)) // out objectHit gets infor we stored on what the Raycast hit
        {
            Debug.Log("You Hit " + objectHit.transform.name); // get name of object you hit

            projectileVisualFeedback.transform.position = objectHit.point; // move the visual to the impact point where you hit the object

            if (objectHit.transform.tag == "Enemy")
            {
                // apply EnemyTakeDamage
                EnemyController _enemy = objectHit.transform.gameObject.GetComponent<EnemyController>();
                if (_enemy != null)
                {
                    Debug.Log("Detected Enemy");
                    _enemy.EnemyTakeDamage(weaponDamage);
                }
            }
            
            if (objectHit.transform.tag == "Key")
            {
                // unlock barriers
                BarrierKey _barrierKey = objectHit.transform.gameObject.GetComponent<BarrierKey>();
                if (_barrierKey != null)
                {
                    Debug.Log("Detected Key");
                    _barrierKey.UnlockBarrier();
                }
            }
            

        } else
        {
            Debug.Log("Miss");
        }
    }

    /* ASSAULT RIFLE - Burst Fire
    on the gun object
	if gun step is greater then 0
		increase step by 1
		
	if gun step is equal to 1, 6, or 11
		call gun fire event (on gun)
		
	if gun step is greater then (total gun cooldown, like 60)
		loop gun step back to 0
    */
}
