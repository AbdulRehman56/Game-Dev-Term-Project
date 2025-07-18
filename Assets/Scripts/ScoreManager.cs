using TMPro;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private int m_points;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    
    // Static reference to access current score
    private static ScoreManager instance;

    // Static event that other scripts can invoke to increase score
    public static Action OnScoreAdded;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        OnScoreAdded += AddScore;
    }

    void OnDisable()
    {
        OnScoreAdded -= AddScore;
    }

    void Start()
    {
        UpdateScoreText();
    }

    private void AddScore()
    {
        score += m_points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    // Static method to get current score
    public static int GetCurrentScore()
    {
        if (instance != null)
            return instance.score;
        return 0;
    }
    
    // Static method to reset score
    public static void ResetScore()
    {
        if (instance != null)
        {
            instance.score = 0;
            instance.UpdateScoreText();
        }
    }
}
