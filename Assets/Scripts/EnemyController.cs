using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyHealthMax = 100;
    public float enemyCurHealth;
    public float lookRadius = 20f; // range at which enemy detects player

    Transform target; // player to target
    //NavMeshAgent agent; // enemy mesh to move around

    public void Start()
    {
        enemyCurHealth = enemyHealthMax;

        target = PlayerManager.instance.player.transform;
        //agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        //enemyCurHealth = Mathf.Clamp(enemyCurHealth, 0, enemyHealthMax);

        // detect player within certain range
        float distance = Vector3.Distance(target.position, transform.position);

        // if sees player, shoot
        if (distance <= lookRadius)
        {
            //agent.SetDestination(target.position); // chase target

            FaceTarget();
            // shoot at player
            // player.takeDamage
        }


        if (enemyCurHealth <= 0)
        {
            KillEnemy();
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

    void EnemyShoot()
    {

    }

    public void KillEnemy()
    {
        Debug.Log("Enemy has been killed!");
        //this.gameObject.SetActive(false);
        // animation/ instantiate here?
        Destroy(gameObject, 0.1f);
    }
    public void EnemyTakeDamage(float takeDamage)
    {
        Debug.Log("Enemy Health: " + enemyCurHealth);
        enemyCurHealth -= takeDamage;
        takeDamage = System.Math.Abs(takeDamage); // is not a negative number
    }
}
