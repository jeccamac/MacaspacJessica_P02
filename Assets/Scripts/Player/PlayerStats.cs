using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthMax = 100;
    public float currentHealth;
    public float staminaMax = 100f;
    public float currentStamina;
    public bool invincibility = false;

    HUDmanager hudManager; // reference HUD to connect to healthSlider and staminaSlider

    public void Awake()
    {
        hudManager = FindObjectOfType<HUDmanager>();

        currentStamina = staminaMax;
        currentHealth = healthMax;
        // clamp ?
    }

    public void FixedUpdate()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, healthMax); // set _currentHealth between these two values: 0 and _maxHealth
        currentStamina = Mathf.Clamp(currentStamina, 0, staminaMax);
        currentStamina += 0.2f;
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        damageAmount = System.Math.Abs(damageAmount); // ensure number is not negative
        Debug.Log("Health is now " + currentHealth);

        // update slider
        hudManager.UpdatePlayerHealth();

        //currentHealth = Mathf.Clamp(currentHealth, 0, healthMax); 
    }

    public void AddHeal(float healAmount)
    {
        currentHealth += healAmount;
        healAmount = System.Math.Abs(healAmount);
        Debug.Log("Health is now " + currentHealth);

        //update slider
        hudManager.UpdatePlayerHealth();
    }

    public void TakeStamina(float minusStamina)
    {
        currentStamina -= minusStamina;
        Debug.Log("Stamina is now " + currentStamina);

        //update slider
        hudManager.UpdatePlayerStamina();
    }

    public void AddStamina(float addStamina)
    {
        currentStamina += addStamina;
        Debug.Log("Stamina is now " + currentStamina);

        //update slider
        hudManager.UpdatePlayerStamina();
    }
}
