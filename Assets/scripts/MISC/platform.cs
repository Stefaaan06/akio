using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the platform
    public float moveDistance = 3f; // Distance the platform moves from its start position

    private Vector3 startPosition;
    private Vector3 previousPosition;
    private Vector3 platformVelocity;

    void Start()
    {
        startPosition = transform.position;
        previousPosition = startPosition;
    }

    void Update()
    {
        // Move the platform to a position and then back
        float newX = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Calculate the platform's velocity
        platformVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }   
        // Apply the platform's velocity to the player's Rigidbody2D
        Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(new Vector2(platformVelocity.x, platformVelocity.y) * 25f, ForceMode2D.Impulse);

        }
    }
}