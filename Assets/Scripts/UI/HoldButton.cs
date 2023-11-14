using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Класс для отслеживания нажатия и отпуска кнопок
public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<HoldButton, PointerEventData> PointerDown; 
    public event Action<HoldButton, PointerEventData> PointerUp; 

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke(this, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke(this, eventData);
    }
}
