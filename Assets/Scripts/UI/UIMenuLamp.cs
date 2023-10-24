using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class UIMenuLamp : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] float _swingRadius;
    [SerializeField] float _swingSpeed = 1.0f;
    Light2D _light;

    float _swingTimer;
    float _initialAngle;
    int _clickCounter;
    bool _disableClick;
    bool _increaseRadius;

    void Start()
    {
        _initialAngle = transform.eulerAngles.z;
        _light = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        _swingTimer += _swingSpeed * Time.deltaTime;
        float unitValue = Mathf.Cos(_swingTimer);
        float angle = _initialAngle + (unitValue * _swingRadius);

        transform.rotation = Quaternion.Euler(angle * Vector3.forward);

        if(_increaseRadius == true)
        {
            float increment = Mathf.Lerp(0.0f, 1.0f, 0.01f);
            _light.pointLightOuterRadius += increment;
            if (increment >= 1.0f)
            {
                _disableClick = true;
                _increaseRadius = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_disableClick == true)
            return;
        _clickCounter++;
        if (_clickCounter >= 5.0f)
            _increaseRadius = true;
    }
}
