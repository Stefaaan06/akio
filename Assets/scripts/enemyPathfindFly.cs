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

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Cast a ray in the direction of movement to detect obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleAvoidanceDistance, obstacleLayer);
        if (hit.collider != null)
        {
            // If an obstacle is detected, adjust the direction to avoid it
            Vector3 avoidDirection = Vector3.Cross(direction, Vector3.forward).normalized;
            direction = (direction + avoidDirection).normalized;
        }

        // Move the enemy towards the player
        transform.position += direction * (speed * Time.deltaTime);

        // Calculate the angle and rotate the enemy to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    
    public void death()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(deathEffect2, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}