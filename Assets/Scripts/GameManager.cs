using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isGamePaused = false;
    private bool gameStarted = false;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        gameStarted = false;
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
        // Only spawn enemies if game is running
        if (!gameStarted || isGamePaused) return;
        
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
}