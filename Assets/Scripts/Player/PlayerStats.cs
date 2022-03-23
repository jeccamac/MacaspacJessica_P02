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

    [SerializeField] GameObject _playerHUD;

    GameObject _deathHUD;
    public GameObject corpsePrefab;
    public GameObject corpseCam;

    HUDmanager hudManager; // reference HUD to connect to healthSlider and staminaSlider

    public void Awake()
    {
        _deathHUD = GameObject.Find("DeathHUD_pnl");
        _deathHUD.SetActive(false);

        // connect playerstats to hud manager
        hudManager = FindObjectOfType<HUDmanager>();

        //currentStamina = staminaMax; // in LevelController
        //currentHealth = healthMax;
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

        _deathHUD.SetActive(true);

        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(corpsePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(corpseCam, new Vector3(0, 0, 0), Quaternion.identity);

        Cursor.lockState = CursorLockMode.None;

        _playerHUD.SetActive(false);

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
