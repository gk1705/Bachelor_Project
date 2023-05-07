using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEvent _hoverEvent;

    private bool _isHovered;

    void Start()
    {
        if (_hoverEvent == null)
        {
            Debug.LogError("Hover event hasn't been assigned.");
        }
    }

    void Update()
    {
        if (_isHovered)
        {
            _hoverEvent.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
    }
}
