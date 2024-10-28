using System;
using TMPro;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    
    public TMP_Text playerSpeed;
    public TMP_Text playerPosition;
    public GameObject debug;

    private void Start()
    {
        if (PlayerPrefs.GetInt("debugInfo") != 1)
        {
            debug.SetActive(false);
            this.enabled = false;
        }
    }

    void Update()
    {
        playerSpeed.text = "Velocity: " + rb.linearVelocity.magnitude.ToString("F2");
        playerPosition.text = "Position: " + player.transform.position.x.ToString("F2") + ", " + player.transform.position.y.ToString("F2");
    }
}
