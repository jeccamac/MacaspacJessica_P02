using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _pauseMenu;

    int _currentScore;

    GameObject Player;
    PlayerStats fpsPlayer;

    public float mouseSensitivity = 200f;

    public void Start()
    {
        Player = GameObject.Find("FPS Player"); // find fps player game object first
        fpsPlayer = Player.GetComponent<PlayerStats>(); // then get component from the object - getComponent wont work unless you have the baseObject attached or called from Find^

    }

    public void Update()
    {
        // Increase Score manually
        // TODO replace with real implementation later
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        // Exit Level
        // TODO bring up popup menu for navigation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            //PauseGame();
            Cursor.lockState = CursorLockMode.None; //ACTIVATE CURSOR
        } else
        {
            _pauseMenu.SetActive(false);
            //ResumeGame();
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (fpsPlayer.currentHealth <= 0)
        {
            fpsPlayer.Kill();
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
        Time.timeScale = 0;
        Debug.Log("Paused Game");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("Resume Game");
    }

    public void QuitGame()
    {
        ExitLevel();
    }
}
