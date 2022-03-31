using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    GameObject pauseMenu;

    public int _currentScore;

    GameObject Player;
    PlayerStats fpsPlayer;

    SettingsMenu settingsMenu; // reference for sliders

    private float maxMouseSense = 300f;
    private float minMouseSense = 100f;
    public float mouseSensitivity;
    public static bool gameIsPaused;    

    public void Start()
    {
        Player = GameObject.Find("FPS Player"); // find fps player game object first
        fpsPlayer = Player.GetComponent<PlayerStats>(); // then get component from the object - getComponent wont work unless you have the baseObject attached or called from Find^        

        pauseMenu = GameObject.Find("PauseMenu_pnl");
        // connect to slider in settings menu
        settingsMenu = pauseMenu.GetComponent<SettingsMenu>();

        pauseMenu.SetActive(false);

        fpsPlayer.currentHealth = fpsPlayer.healthMax;
        fpsPlayer.currentStamina = fpsPlayer.staminaMax;
        
    }

    public void Update()
    {
        // IncreaseScore();
        // TODO replace with real implementation later
        
        // Popup menu for navigation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
            mouseSensitivity = Mathf.Clamp(mouseSensitivity, minMouseSense, maxMouseSense);

        } 
    }

    public void ExitLevel()
    {
        // compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            // save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }

        // load new level
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int scoreIncrease)
    {
        // increase score
        _currentScore += scoreIncrease;
        // update score display so we can see the new score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Paused Game");
            pauseMenu.SetActive(true);

            settingsMenu.UpdateMouseSlider();
            settingsMenu.UpdateVolumeSlider();

            Cursor.lockState = CursorLockMode.None; //ACTIVATE CURSOR

        } else
        {
            Time.timeScale = 1;
            Debug.Log("Resume Game");
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
    }
    }
    
    /*
    public void ResumeGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 1;
            Debug.Log("Resume Game");
            _pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    */

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void QuitGame()
    {
        ExitLevel();
        gameIsPaused = gameIsPaused; // dont pause after exiting to menu, character freezes
    }
}
