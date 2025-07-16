using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPoint;
    public float destroyTime = 5f;
    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gm = Instantiate(missile, missileSpawnPoint.position, missileSpawnPoint.rotation);
            // gm.transform.SetParent(null);
            Destroy(gm, 5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle player collision with enemy
            Debug.Log("Player hit by an enemy!");
            // You can add logic here to handle player damage or game over
            Destroy(this.gameObject); // Destroy the player object
        }
    }
}
