
using UnityEngine;
using UnityEngine.Events;

public class callEventOnPlayerEnter : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private bool enter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(Time.deltaTime);   
        if (other.CompareTag("Player"))
        {
            onTriggerEnter.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerExit.Invoke();
        }
    }
}
