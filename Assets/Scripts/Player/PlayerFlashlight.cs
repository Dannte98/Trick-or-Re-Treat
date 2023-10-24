using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFlashlight : MonoBehaviour, IRecharge
{
    [Header("Rotation")]
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _angleOffset;
    Vector2 _mousePos;

    [Header("Light")]
    [SerializeField] float _range;
    [SerializeField] float _maxChargeTime;
    [SerializeField] float _rechargeFactor;

    Light2D _flashlight;
    Collider2D _flashlightCollider;
    bool _isFlashing;
    float _flashTimer;
    IMove _playerMovement;

    public float UseTime => _maxChargeTime;
    public float RemainingTime => _flashTimer;

    void Start()
    {
        _flashlight = GetComponentInChildren<Light2D>();
        _flashlightCollider = GetComponent<Collider2D>();
        _flashlight.pointLightOuterRadius = _range;
        _flashTimer = _maxChargeTime;
        _playerMovement = GetComponentInParent<IMove>();
    }

    void Update()
    {
        GetInput();
        Rotate();
        UpdateRechargeTimer();
        Flashlight();
    }

    private void GetInput()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _isFlashing = Input.GetMouseButton(1);
    }

    private void Rotate()
    {
        //Get angle from mouse to players position
        Vector2 unitVector = (_mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(unitVector.y, unitVector.x) * Mathf.Rad2Deg + _angleOffset;
        float rotateAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, _rotateSpeed * Time.deltaTime);
        transform.eulerAngles = rotateAngle * Vector3.forward;
    }

    public void UpdateRechargeTimer()
    {
        //Check input and if there is available time
        if (_isFlashing == true)
        {
            _flashTimer -= Time.deltaTime;
            if (_flashTimer < 0.0f)
                _flashTimer = 0.0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.R) && _flashTimer < _maxChargeTime)
            {
                _playerMovement.CanMove = false;
                _flashTimer += _rechargeFactor * Time.deltaTime;
                //---------------------
                //Play recharge animation
                //---------------------
            }
            else
                _playerMovement.CanMove = true;
        }
    }

    void Flashlight()
    {
        //Enable light and collider 
        if (_flashTimer > 0.1f && _isFlashing)
        {
            if(_flashlight.enabled == false)
                _flashlight.enabled = true;
            if (_flashlightCollider.enabled == false)
                _flashlightCollider.enabled = true;
        }
        else
        {
            if (_flashlight.enabled == true)
                _flashlight.enabled = false;
            if (_flashlightCollider.enabled == true)
                _flashlightCollider.enabled = false;
        }
    }
}
