using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isHold;
    public UnityEvent _onPointerDown;
    public UnityEvent _onPointerUp;
    public UnityEvent _onPointerUpdate;

    public void OnPointerDown(PointerEventData eventData)
    {
        _onPointerDown.Invoke();
        isHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _onPointerUp.Invoke();
        isHold = false;
    }
    private void Update()
    {
        if (isHold)
        {
            _onPointerUpdate.Invoke();
        }
    }
}
