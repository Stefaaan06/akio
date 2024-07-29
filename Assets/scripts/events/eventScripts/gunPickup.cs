using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    public gun gun;

    private playerGun _playerGun;
    
    private void Start()
    {
        _playerGun = FindObjectOfType<playerGun>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger entered");
        Debug.Log("other: " + other);
        
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            addGun();
        }
    }

    public void addGun()
    {
        _playerGun.AddGun(gun);
    }
}
