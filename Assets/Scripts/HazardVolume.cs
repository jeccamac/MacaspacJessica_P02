using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HazardVolume : MonoBehaviour
{
    public AudioSource sndBoom;
    public float lookRadius = 10f; // range at which enemy detects player
    Transform target; // player to target
    NavMeshAgent agent; // enemy mesh to move around

    GameObject _levelObject;
    Level01Controller _levelScore;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        _levelObject = GameObject.Find("LevelController");
        _levelScore = _levelObject.GetComponent<Level01Controller>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // if sees player, shoot
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position); // chase target

            FaceTarget();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerStats thisPlayer = other.gameObject.GetComponent<PlayerStats>();

        // if we found player, continue
        if (thisPlayer != null)
        {
            // if player is not invincible
            if (thisPlayer.invincibility == false)
            {
                Debug.Log("You took a hit!");
                // hit player
                thisPlayer.TakeDamage(50);

                sndBoom.Play();

                Destroy(gameObject, 1f);
            }
            else { Debug.Log("Invincible!"); }

        }
    }

    public void BombDestroy()
    {
        Debug.Log("Bomb has been destroyed!");
        sndBoom.Play();
        //add score
        _levelScore.IncreaseScore(10);
        Destroy(gameObject, 1f);
    }
}
