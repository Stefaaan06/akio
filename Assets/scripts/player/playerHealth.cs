using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 3;

    public GameObject[] healthObjects;
    public Rigidbody2D rb;
    public float collisionForce = 100f;
    public float upwardForce = 200f; // New variable for upward force

    public PlayerUIManager uiManager;
    public PlayerSoundManager soundManager;
    public PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(playerMovement.isDashing && other.gameObject.layer == 12) 
            {
                other.gameObject.GetComponent<health>().removeHP(50);
                if (playerMovement.isUpwardsDashing)
                {
                    rb.AddForce(new Vector2(0, upwardForce / 3)); 
                }
                else if (playerMovement.isDownwardsDashing)
                {
                    rb.AddForce(new Vector2(0, upwardForce * 2));
                }
                else
                {
                    rb.AddForce(new Vector2(0, upwardForce));
                }
                return;
            }
            health--;
            setHealth(health);
            rb.AddForce(collisionForce * (transform.position - other.transform.position).normalized);
            soundManager.playDamageSound();
        }
    }

    void setHealth(int newHealth)
    {
        health = newHealth;
        for (int i = 0; i < healthObjects.Length; i++)
        {
            if (i < health)
            {
                healthObjects[i].SetActive(true);
            }
            else
            {
                healthObjects[i].SetActive(false);
            }
        }
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        uiManager.death();
    }
}