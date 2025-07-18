using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI pointText;
    public Button restartButton;
    
    [Header("Scene Management")]
    public string mainGameSceneName = "GameScene"; // Name of your main game scene

    private void Awake()
    {
        Debug.Log("GameOverScript Awake() called");
        
        // Try to automatically find components if not assigned
        if (pointText == null)
        {
            pointText = GetComponentInChildren<TextMeshProUGUI>();
            if (pointText != null)
                Debug.Log("Automatically found TextMeshProUGUI component: " + pointText.name);
            else
                Debug.LogWarning("Could not find TextMeshProUGUI component automatically");
        }
        else
        {
            Debug.Log("PointText was already assigned: " + pointText.name);
        }
        
        if (restartButton == null)
        {
            restartButton = GetComponentInChildren<Button>();
            if (restartButton != null)
                Debug.Log("Automatically found Button component: " + restartButton.name);
            else
                Debug.LogWarning("Could not find Button component automatically");
        }
        else
        {
            Debug.Log("RestartButton was already assigned: " + restartButton.name);
        }
        
        // Set up button listeners in Awake instead of Start
        if (restartButton != null)
        {
            // Clear any existing listeners first
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("Restart button listener added successfully to button: " + restartButton.name);
            
            // Also add a test listener to see if button clicks are working at all
            restartButton.onClick.AddListener(() => Debug.Log("Button clicked - any listener working!"));
        }
        else
        {
            Debug.LogError("Restart Button is not assigned and could not be found automatically! Please assign it in the Inspector.");
        }
    }

    private void Start()
    {
        Debug.Log("GameOverScript Start() called");
        // Get the score from PlayerPrefs and display it
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        Debug.Log("Retrieved LastScore from PlayerPrefs: " + lastScore);
        setUp(lastScore);
    }

    public void setUp(int score)
    {
        Debug.Log("Setting up Game Over UI with score: " + score);
        
        // Activate this game object
        gameObject.SetActive(true);
        
        if (pointText != null)
        {
            pointText.text = score.ToString();
            Debug.Log("Score text updated to: " + pointText.text);
        }
        else
        {
            Debug.LogError("Point Text is not assigned in GameOverScript! Please assign it in the Inspector.");
            Debug.LogError("Score was: " + score + " but cannot display it without pointText component.");
        }
    }

    public void HideGameOver()
    {
        gameObject.SetActive(false);
    }
    
    public void RestartGame()
    {
        Debug.Log("RestartGame method called!");
        
        Time.timeScale = 1f; // Make sure time scale is reset
        
        // Set a flag to indicate we're restarting from game over
        PlayerPrefs.SetInt("RestartFromGameOver", 1);
        PlayerPrefs.Save();
        
        Debug.Log("Loading scene: " + mainGameSceneName);
        SceneManager.LoadScene(mainGameSceneName);
    }
    
    // Alternative method for Unity Inspector OnClick events
    public void OnRestartButtonClick()
    {
        Debug.Log("OnRestartButtonClick method called from Unity Inspector!");
        RestartGame();
    }
}