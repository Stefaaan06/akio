using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upBoost : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 100;
    void Start()
    {
        rb = FindObjectOfType<PlayerMovement>().rb;       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.AddForce(force * Vector2.up);
        }
    }
}
