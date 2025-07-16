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

    private float minX, maxX, minY, maxY;

    private void Start()
    {
        // Get the camera reference
        Camera cam = Camera.main;

        // Distance from camera to player (z=0)
        float zDistance = Mathf.Abs(cam.transform.position.z + transform.position.z);

        // Convert screen bounds to world bounds
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zDistance));

        // Clamp values based on the player’s half width/height
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        minX = bottomLeft.x + halfWidth;
        maxX = topRight.x - halfWidth;
        minY = bottomLeft.y + halfHeight;
        maxY = topRight.y - halfHeight;
    }

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

        // Clamp position
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        transform.position = clampedPos;
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gm = Instantiate(missile, missileSpawnPoint.position, missileSpawnPoint.rotation);
            Destroy(gm, destroyTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by an enemy!");
            Destroy(this.gameObject);
        }
    }
}
