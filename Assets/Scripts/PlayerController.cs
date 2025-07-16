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
        Camera cam = Camera.main;
        float zDistance = Mathf.Abs(cam.transform.position.z + transform.position.z);

        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zDistance));

        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        minX = bottomLeft.x + halfWidth;
        maxX = topRight.x - halfWidth;
        minY = bottomLeft.y + halfHeight;
        maxY = topRight.y - halfHeight;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        KeyboardMovement(); // For testing in Unity Editor
#else
        TouchMovement();    // For mobile
#endif
        PlayerShoot();
    }

    void KeyboardMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
        ClampPosition();
    }

    void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Mathf.Abs(Camera.main.transform.position.z)));

            // Move towards touch position
            Vector3 targetPos = new Vector3(touchWorldPos.x, touchWorldPos.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);

            ClampPosition();
        }
    }

    void ClampPosition()
    {
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        transform.position = clampedPos;
    }

    void PlayerShoot()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootMissile();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ShootMissile();
            }
        }
#endif
    }

    void ShootMissile()
    {
        GameObject gm = Instantiate(missile, missileSpawnPoint.position, missileSpawnPoint.rotation);
        Destroy(gm, destroyTime);
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
