using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class callEventOnStartWithDelay : MonoBehaviour
{
    public UnityEvent unityEvent;
    public float delayTime;
    void Start()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(delayTime);
        unityEvent.Invoke();
    }
}
