using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDmanager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider staminaSlider;

    public void Update()
    {
        UpdatePlayerHealth();
        UpdatePlayerStamina();
    }

    public void UpdatePlayerHealth()
    {
        // set slider value equal to health Slider value
        healthSlider.value = playerStats.currentHealth;
    }

    public void UpdatePlayerStamina()
    {
        // set slider value equal to stamina Slider value
        staminaSlider.value = playerStats.currentStamina;
    }
}
