using System;
using UnityEngine;

public class Enemy : HasHealth, IMove
{
    [Header("Movement")]
    [SerializeField] float _moveSpeed;
    Transform _playerTransform;
    bool _canMove = true;
    public bool CanMove { set => _canMove = value; }

    [Header("Damage")]
    [SerializeField] float _damage;

    [Header("Animation")]
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Death")]
    [SerializeField] GameObject _deathParticles;
    public float Damage => _damage;

    protected override void Start()
    {
        base.Start();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        this.OnDeath += Dead;
    }

    void Update()
    {
        if (_isDead == true)
            return;
        Movement();
    }

    public void Movement()
    {
        if(_canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position , _moveSpeed * Time.deltaTime);
            if (transform.position.x < _playerTransform.position.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
            _animator.speed = 1.0f;
        }
        else
        {
            _animator.speed = 0.0f;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == true)
        {
            collision.collider.GetComponent<HasHealth>().TakeDamage(_damage);
            Death();
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Freezes"))
            _canMove = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Freezes"))
            _canMove = true;
    }

    void Dead()
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        AudioManager.Instance.Play("EnemyDeath");
        Destroy(gameObject);
    }
}