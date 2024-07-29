using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsLimiter : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 140;
    }

}
