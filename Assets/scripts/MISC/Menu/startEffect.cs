using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class startEffect : MonoBehaviour
{
    public PostProcessVolume postProcessLayer;
    private ChromaticAberration _ca;
    private Vignette _v;
    void Start()
    {
        postProcessLayer.profile.TryGetSettings(out _ca);
        postProcessLayer.profile.TryGetSettings(out _v);
        StartCoroutine(waitTillNext());
    }

    void FixedUpdate()
    {
        if(_ca.intensity.value > 5) return;
        _ca.intensity.value += 0.01f;
    }

    IEnumerator waitTillNext()
    {
        yield return new WaitForSeconds(1.8f);
        while (_v.intensity.value < 1)
        {
            _v.intensity.value += 0.02f;
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
