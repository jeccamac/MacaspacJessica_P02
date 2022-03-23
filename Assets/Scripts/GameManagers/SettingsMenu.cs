using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Slider _settingsMouseSlider;
    [SerializeField] Slider _settingsVolumeSlider;

    GameObject _lvlController;
    Level01Controller _mouseSense;

    public void Start()
    {
        _lvlController = GameObject.Find("LevelController");
        _mouseSense = _lvlController.GetComponent<Level01Controller>();

        _audioSource = FindObjectOfType<AudioSource>();
    }

    public void Update()
    {
        UpdateMouseSlider();
        UpdateVolumeSlider();
    }

    public void UpdateMouseSlider()
    {
        _settingsMouseSlider.value = _mouseSense.mouseSensitivity; // get mouseSensitivity from lvlcontroller
    }

    public void UpdateVolumeSlider()
    {
        _settingsVolumeSlider.value = _audioSource.volume;
    }
}
