using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMove, IRecharge
{
    #region MOVEMENT
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 5.0f;
    [Range(1.0f, 2.0f)][SerializeField] float _dashFactor = 1.0f;
    [SerializeField] float _dashTime = 3.0f;
    [SerializeField] float _dashRechargeFactor = 1.0f;

    Rigidbody2D _rigidbody2D;
    bool _canMove = true;
    float _horizontal;
    float _vertical;
    bool _isDashing;
    float _dashTimer;
    public bool CanMove { set => _canMove = value; }
    public float UseTime => _dashTime;
    public float RemainingTime => _dashTimer;
    #endregion

    #region ANIMATION
    [Header("Animation")]
    [SerializeField] Animator _playerAnimator;
    #endregion

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dashTimer = _dashTime;
    }

    void Update()
    {
        GetInput();
        UpdateRechargeTimer();
    }

    void FixedUpdate()
    {
        Movement();
    }
    

    void GetInput()
    {
        //Get all input to process
        _horizontal = Input.GetAxisRaw("Horizontal");
        _playerAnimator.SetFloat("DirectionX", _horizontal);
        _vertical = Input.GetAxisRaw("Vertical");
        _playerAnimator.SetFloat("DirectionY", _vertical);
        _isDashing = Input.GetKey(KeyCode.LeftShift);
    }

    public void Movement()
    {
        //Define rigid body velocity according to input
        if (_canMove == true)
        {
            float speedFactor = (_dashTimer > 0.1f && _isDashing) ? _dashFactor : 1.0f;
            _rigidbody2D.velocity = _moveSpeed * speedFactor * Time.deltaTime * new Vector2(_horizontal, _vertical).normalized;
        }
        else
            _rigidbody2D.velocity = Vector2.zero;
    }

    public void UpdateRechargeTimer()
    {
        //Check input and if there is available time
        if (_isDashing == true)
        {
            _dashTimer -= Time.deltaTime;
            if (_dashTimer < 0.0f)
                _dashTimer = 0.0f;
        }
        else
        {
            if (_dashTimer < _dashTime)
                _dashTimer += _dashRechargeFactor * Time.deltaTime;
            else if (_dashTimer > _dashTime)
                _dashTimer = _dashTime;
        }
    }
}
