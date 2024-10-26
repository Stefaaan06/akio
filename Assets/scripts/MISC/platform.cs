using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the platform
    public float moveDistance = 3f; // Distance the platform moves from its start position

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the platform to a position and then back
        float newX = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}