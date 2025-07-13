using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minInstantiateValue = -8f;  // Set default values
    public float maxInstantiateValue = 8f;   // Set default values
    public float enemyDestroyTime = 10f;

    private void Start()
    {
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
    }

    void InstantiateEnemy()
    {
        float randomX = Random.Range(minInstantiateValue, maxInstantiateValue);
        Vector3 enemypos = new Vector3(randomX, 6f, 0f);
        
        // Debug to see if random values are working
        // Debug.Log("Spawning enemy at X: " + randomX);
        
        GameObject enemy = Instantiate(enemyPrefab, enemypos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestroyTime);
    }
}