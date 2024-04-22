using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float homingStrength = 5f;
    public float rotateSpeed = 200f;
    public string enemyTag = "EnemyTag";
    private Rigidbody2D rb;

    public Transform target;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target == null)
        {
            FindNearestEnemy();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

    private void MoveTowardsTarget()
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        //Vector3 velocity = direction * speed;
        //transform.Translate(velocity * Time.deltaTime);

        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    
    }
}
