using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    UIManager uiManager;
    private void Awake()
    {
        // fill our references
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DamagePlayer(10);
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        // subtract from health
        health -= damageAmount;
        Debug.Log("Health is now " + health);

        // update the slider
        uiManager.UpdateHealth();

    }
}
