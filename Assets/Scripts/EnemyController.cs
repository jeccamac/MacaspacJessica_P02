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
    public AudioSource sndEnemyDeath;
    public AudioSource sndEnemyShoot;

    Transform target; // player to target
    //NavMeshAgent agent; // enemy mesh to move around

    [Tooltip("The start position of the ray to fire, obj projectile start")]
    [SerializeField] Transform enemyRayOrigin;

    [Tooltip("How far to shoot")]
    [SerializeField] float enemyShootDist = 20f;

    [Tooltip("Visual feedback when firing, can be light or particles")]
    [SerializeField] TrailRenderer bulletTrail;

    [SerializeField] int enemyWeaponDamage = 20;
    public float shootDelay = 20f;
    private float lastShootTime;
    bool canShoot = true;

    GameObject _lvlObj;
    Level01Controller _lvlScore;

    //[Tooltip("What objects in the layer you are able to hit")]
    //[SerializeField] LayerMask hitLayers;

    RaycastHit enemyHit; // stores info about our RaycastHit

    public void Start()
    {
        enemyCurHealth = enemyHealthMax;

        target = PlayerManager.instance.player.transform;
        //agent = GetComponent<NavMeshAgent>();

        _lvlObj = GameObject.Find("LevelController");
        _lvlScore = _lvlObj.GetComponent<Level01Controller>();

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
            EnemyShoot();
            
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
        if (canShoot == true)
        {
            // shoot at player
            Vector3 enemyRayDir = (target.position - transform.position).normalized;

            Debug.DrawRay(enemyRayOrigin.position, enemyRayDir * enemyShootDist, Color.red, 1f);

            sndEnemyShoot.Play();
            TrailRenderer trail = Instantiate(bulletTrail, enemyRayOrigin.position, Quaternion.identity); // visual bullet trail
                                                                                                          // fire the Raycast
            if (Physics.Raycast(enemyRayOrigin.position, enemyRayDir, out enemyHit, enemyShootDist)) // out objectHit gets info we stored on what the Raycast hit
            {
                Debug.Log("You Hit " + enemyHit.transform.name); // get name of object you hit
                StartCoroutine(SpawnTrail(trail, enemyHit));

                if (enemyHit.transform.tag == "Player")
                {
                    // apply EnemyTakeDamage
                    PlayerStats _player = enemyHit.transform.gameObject.GetComponent<PlayerStats>();
                    if (_player != null)
                    {
                        Debug.Log("Detected Player");
                        _player.TakeDamage(enemyWeaponDamage); // player.takeDamage
                    }
                }
            }
            else
            {
                Debug.Log("Enemy Miss");
            }
            canShoot = false;
            StartCoroutine(ReShoot());
        }
    }

    IEnumerator ReShoot()
    {
        yield return new WaitForSecondsRealtime(2f);
        canShoot = true;
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

    public void KillEnemy()
    {
        Debug.Log("Enemy has been killed!");
        sndEnemyDeath.Play();
        //add score
        _lvlScore.IncreaseScore(10);
        //this.gameObject.SetActive(false);
        // animation/ instantiate here?
        Destroy(gameObject, 1f);
    }
    public void EnemyTakeDamage(float takeDamage)
    {
        Debug.Log("Enemy Health: " + enemyCurHealth);
        enemyCurHealth -= takeDamage;
        takeDamage = System.Math.Abs(takeDamage); // is not a negative number
    }
}
