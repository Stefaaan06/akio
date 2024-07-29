using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class applyPostProccessingEffects : MonoBehaviour
{
    public PostProcessVolume postProcessLayer;
    private MotionBlur _mb;

    private void Awake()
    {
        postProcessLayer.profile.TryGetSettings(out _mb);
        int motionBlurr = PlayerPrefs.GetInt("motionBlur");
        if (motionBlurr == 0)
        {
            _mb.active = true;
        }
        else
        {
            _mb.active = false;
        }
    }

    public void UpdateEffects()
    {
        int motionBlurr = PlayerPrefs.GetInt("motionBlur");
        if (motionBlurr == 0)
        {
            _mb.active = true;
        }
        else
        {
            _mb.active = false;
        }
    }
}
