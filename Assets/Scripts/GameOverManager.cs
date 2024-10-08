using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Assign the Game Over panel in the Inspector
    public GameObject startPanel;     // Assign the Start Game panel in the Inspector
    public Button playButton;         // Assign the Play button in the Inspector
    public Button restartButton;      // Assign the Restart button in the Inspector
    public Button quitButton;         // Assign the Quit button in the Inspector

    private bool gameStarted = false; // Track if the game has started

    void Start()
    {
        // Show the start panel and hide the game over panel at the beginning
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0; // Stop the game time

        // Add listeners to the buttons
        playButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (gameStarted)
        {
            // Game logic goes here
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game time
        gameStarted = true;
    }

    public void GameOver()
    {
        if (gameStarted)
        {
            // Show the Game Over panel
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; // Stop the game time
            gameStarted = false; // Ensure game doesn't restart immediately
        }
    }

    public void RestartGame()
    {
        // Resume the game time and reload the current scene
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Exit the application
        Application.Quit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }
}

