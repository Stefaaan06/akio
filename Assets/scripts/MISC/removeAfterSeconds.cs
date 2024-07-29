using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeAfterSeconds : MonoBehaviour
{
    public float seconds;
    private bool start = false;
    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(waitTilLRemove());
    }

    IEnumerator waitTilLRemove()
    {
        yield return new WaitForSeconds(seconds);
        start = true;
    }
    
    public float shrinkDuration = 4f; // Duration in seconds
    private Vector3 originalScale;
    private float timer = 0f;

    private void Update()
    {
        if(!start) return;
        if (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / shrinkDuration);
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
