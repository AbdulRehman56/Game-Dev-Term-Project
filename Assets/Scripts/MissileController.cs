using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the missile
        }        
    }
}
