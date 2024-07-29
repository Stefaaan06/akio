using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public TMP_Text text;

    public bool larger = false;

    private Vector3 _startSize;
    public void Start()
    {
        _startSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (larger)
        {
            transform.Translate(0,0,-0.05f);
            transform.localScale = _startSize + (_startSize / 4);
        }
        text.color = Color.white;
        button.image.color = Color.black;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        reset();
    }


    public void reset()
    {
        if (larger)
        {
            transform.Translate(0,0,0.05f);
            transform.localScale = _startSize;
        }
        text.color = Color.black;
        button.image.color = Color.white;
    }
}
