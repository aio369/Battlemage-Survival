/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(Rigidbody2D))]
public class HomingProjectile : MonoBehaviour
{
    public Transform target;

    public float speed = 2f;
    public float rotateSpeed = 200f;
    private Rigidbody2D rb;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FiexedUpdate () {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D ()
    {
        Destroy(gameObject);
    }
}
*/