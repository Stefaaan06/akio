using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointManager : MonoBehaviour
{
    public GameObject[] checkpoints;
    public GameObject player;
    private void Start()
    {
        int checkpointNumber = PlayerPrefs.GetInt("checkpoint");
        player.transform.position = checkpoints[checkpointNumber].transform.position;
    }
}
