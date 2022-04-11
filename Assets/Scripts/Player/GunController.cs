using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [Tooltip("The start position of the ray to fire, obj projectile start")]
    [SerializeField] Transform rayOrigin;

    [Tooltip("How far to shoot")]
    [SerializeField] float shootDistance = 20f;

    [Tooltip("Visual feedback when firing, can be light or particles")]
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] ParticleSystem impactParticle;

    [SerializeField] int weaponDamage = 20;
    public AudioSource sndShoot;
    //[Tooltip("What objects in the layer you are able to hit")]
    //[SerializeField] LayerMask hitLayers;

    RaycastHit objectHit; // stores info about our RaycastHit

    private float lastShootTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    // fire the weapon using Raycast
    public void Shoot()
    {
        // calculate direction to shoot the Ray
        Vector3 rayDirection = playerCamera.transform.forward;

        // cast a Debug Ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f); // start position, direction & distance, color, how long visual before disappearing

        sndShoot.Play();
        TrailRenderer trail = Instantiate(bulletTrail, rayOrigin.position, Quaternion.identity); // visual bullet trail

        // fire the Raycast
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance)) // out objectHit gets info we stored on what the Raycast hit
        {            
            Debug.Log("You Hit " + objectHit.transform.name); // get name of object you hit
            StartCoroutine(SpawnTrail(trail, objectHit));
            
            Instantiate(impactParticle, objectHit.point, Quaternion.identity);

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

            if (objectHit.transform.tag == "Bomb")
            {
                // apply BombTakeDamage
                HazardVolume _hazard = objectHit.transform.gameObject.GetComponent<HazardVolume>();
                if (_hazard != null)
                {
                    Debug.Log("Detected Bomb");
                    _hazard.BombDestroy();
                }
            }

            if (objectHit.transform.tag == "KeyShield")
            {
                // unlock barriers
                BarrierKey_Bridge _shieldKey = objectHit.transform.gameObject.GetComponent<BarrierKey_Bridge>();
                if (_shieldKey != null)
                {
                    Debug.Log("Detected ShieldKey");
                    _shieldKey.UnlockShields();
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
        }
        else
        {
            Debug.Log("Miss");
        }
    }

    
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        Trail.transform.position = Hit.point;

        Destroy(Trail.gameObject, Trail.time);
    }
    
   
    /* ASSAULT RIFLE - Burst Fire
    on the gun object
	if gun step is greater then 0
		increase step by 1
		
	if gun step is equal to 1, 6, or 11
		call gun fire event (on gun)
		
	if gun step is greater then (total gun cooldown, like 60)
		loop gun step back to 0

    make sure you make it so that you cant shoot the gun again until gun step = 0
    or else you can rapid fire the gun by mashing click
    you might also want to make it so that you have to RELEASE the trigger to reset to 0, otherwise it loops back to -1
    so the burst is semi auto instead of full

    count++;

    */
}
