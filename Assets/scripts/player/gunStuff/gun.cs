using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "gun", menuName = "ScriptableObjects/Gun", order = 1)]
public class gun : ScriptableObject
{
    [Header("Gun")]
    public string gunName;
    public GameObject gunPrefab;
    
    [Header("Stats")]
    public int damage;
    
    public int range;
    public int maxRange;
    
    public int fireRate;
    
    public int maxClipSize;
    public int currentClipSize;
    
    public int feedback;
    public float reloadTime;
    
    public bool allowAutoFire;
}
