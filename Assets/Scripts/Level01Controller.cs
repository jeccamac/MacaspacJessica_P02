﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;
    private void Update()
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
            ExitLevel();
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
}
