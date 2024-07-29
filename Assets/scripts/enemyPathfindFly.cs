using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathfindFly : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 5f; // Speed of the enemy
    public float obstacleAvoidanceDistance = 2f; // Distance to check for obstacles
    public LayerMask obstacleLayer; // Layer mask to identify obstacles

    public GameObject deathEffect;
    public GameObject deathEffect2;
    public cameraShake cameraShake;

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Check for obstacles in the direction of movement
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleAvoidanceDistance, obstacleLayer);
        if (hit.collider != null)
        {
            // Adjust direction to avoid obstacle
            Vector3 hitNormal = hit.normal;
            direction = Vector3.Reflect(direction, hitNormal).normalized;
        }

        // Move the enemy in the adjusted direction
        transform.position += direction * (speed * Time.deltaTime);

        // Calculate the angle and rotate the enemy to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void death()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(deathEffect2, transform.position, Quaternion.identity);
        cameraShake.ShakeOnce(.4f, .1f);

        Destroy(gameObject);
    }
}