using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minInstantiateValue = -8f;  // Set proper default values
    public float maxInstantiateValue = 8f;   // Set proper default values
    public float enemyDestroyTime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;

    [Header("Game Over")]
    public string gameOverSceneName = "gameover"; // Name of your game over scene

    private bool isGamePaused = false;
    private bool gameStarted = false;
    private bool isGameOver = false;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Check if we're restarting from game over
        bool restartFromGameOver = PlayerPrefs.GetInt("RestartFromGameOver", 0) == 1;
        
        if (restartFromGameOver)
        {
            // Clear the flag
            PlayerPrefs.SetInt("RestartFromGameOver", 0);
            PlayerPrefs.Save();
            
            // Start the game immediately without showing start menu
            StartGameButton();
        }
        else
        {
            // Normal game start - show start menu
            startMenu.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 0f;
            gameStarted = false;
        }
    }

    private void Update()
    {
        // Only allow pause/unpause when game has started
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void InstantiateEnemy()
    {
        // Only spawn enemies if game is running and not game over
        if (!gameStarted || isGamePaused || isGameOver) return;
        
        float randomX = Random.Range(minInstantiateValue, maxInstantiateValue);
        Vector3 enemypos = new Vector3(randomX, 6f, 0f);
        
        Debug.Log("Spawning enemy at X: " + randomX);
        
        GameObject enemy = Instantiate(enemyPrefab, enemypos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestroyTime);
    }

    public void StartGameButton()
    {
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        gameStarted = true;
        isGamePaused = false;
        isGameOver = false;
        
        // Reset score when starting a new game
        ScoreManager.ResetScore();
        
        // Start enemy spawning when game starts
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        
        if (isGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void PauseGame(bool isPaused)
    {
        isGamePaused = isPaused;
        
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        PauseGame(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameStarted = false;
        
        // Stop enemy spawning
        CancelInvoke("InstantiateEnemy");
        
        // Stop time
        Time.timeScale = 0f;
        
        // Get current score and load game over scene
        int currentScore = ScoreManager.GetCurrentScore();
        
        // Store score for the game over scene
        PlayerPrefs.SetInt("LastScore", currentScore);
        PlayerPrefs.Save();
        
        // Load game over scene
        StartCoroutine(LoadGameOverScene());
    }
    
    private IEnumerator LoadGameOverScene()
    {
        // Wait a moment for explosion effect
        yield return new WaitForSecondsRealtime(1f);
        
        // Load the game over scene
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
}