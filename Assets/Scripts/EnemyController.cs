using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private float speed = 1f;
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
