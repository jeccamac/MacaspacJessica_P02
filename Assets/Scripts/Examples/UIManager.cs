using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] SliderRotator sliderRotator;
    [SerializeField] Slider _slider;

    public Slider healthSlider;
    public PlayerHealth playerHealth;
    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        // change speed to match the slider's value
        sliderRotator.rotateSpeed = _slider.value;
    }

    public void UpdateHealth()
    {
        // set slider value equal to health
        healthSlider.value = playerHealth.health;
    }
}
