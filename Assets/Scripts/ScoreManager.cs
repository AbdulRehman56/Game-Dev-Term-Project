using TMPro;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private int m_points;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    // Static event that other scripts can invoke to increase score
    public static Action OnScoreAdded;

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
}
