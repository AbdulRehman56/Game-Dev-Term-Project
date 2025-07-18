using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartScript : MonoBehaviour
{
    void Start()
    {
        // Optionally, you can set up any initial state or references here
        Debug.Log("RestartScript Start() called");
    }

    void Update()
    {
    }

    public void restartGame()
    {

        SceneManager.LoadScene("GameScene");
    }
    
    public void quitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}