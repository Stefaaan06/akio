using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class mainMenuStartEffect : MonoBehaviour
{
    public PostProcessVolume postProcessLayer;
    private Vignette _v;
    void Start()
    {
        postProcessLayer.profile.TryGetSettings(out _v);
        StartCoroutine(waitTillnext());

    }

    IEnumerator waitTillnext()
    {
        while (_v.intensity.value > 0)
        {
            _v.intensity.value -= 0.02f;
            yield return new WaitForFixedUpdate();
        }
    }
}
