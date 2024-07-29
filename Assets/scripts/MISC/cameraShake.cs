using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    
    public void ShakeOnce(float amplitude = .1f, float time = .05f)
    {
        if(PlayerPrefs.GetInt("cameraShake") == 0) return;
        StartCoroutine(shakeEnum(amplitude, time));
    }

    private IEnumerator shakeEnum(float amplitude, float time)
    {
        var noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = amplitude;
        noise.enabled = true;
        yield return new WaitForSeconds(time);
        noise.m_AmplitudeGain = 0;
    }
        
}
