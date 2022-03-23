using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyHealthMax = 100;
    public float enemyCurHealth;

    public void Awake()
    {
        enemyCurHealth = enemyHealthMax;
        enemyCurHealth = Mathf.Clamp(enemyCurHealth, 0, enemyHealthMax);
    }

    public void KillEnemy()
    {
        Debug.Log("Enemy has been killed!");
        this.gameObject.SetActive(false);
    }
    public void EnemyTakeDamage(float takeDamage)
    {
        enemyCurHealth -= takeDamage;
        takeDamage = System.Math.Abs(takeDamage); // is not a negative number
    }
}
