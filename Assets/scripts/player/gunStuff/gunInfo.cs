using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunInfo : MonoBehaviour
{
    public ParticleSystem[] muzzleFlash;
    
    public void Shoot()
    {
        foreach (ParticleSystem flash in muzzleFlash)
        {
            flash.Play();
        }
    }
}
