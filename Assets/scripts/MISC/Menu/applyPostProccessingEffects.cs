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
        UpdateEffects();
    }

    //reduncany cus of my retarded code <3 
    private void Start()
    {
        UpdateEffects();
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
        int qualityLevel = QualitySettings.GetQualityLevel();
        if(qualityLevel == 0)
        {
            postProcessLayer.enabled = false;
        }else
        {
            postProcessLayer.enabled = true;
        }
    }
}
