using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hAS rUN");
            ScoreManager.OnScoreAdded?.Invoke();
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the missile
        }        
    }
}
