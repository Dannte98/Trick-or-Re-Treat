using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyProjectile : MonoBehaviour
{
    [SerializeField] GameObject _explosionEffect;
    [SerializeField] float _lifeTime = 3.0f;
    Rigidbody2D _candyRigidbody;
    SpriteRenderer _spriteRenderer;
    
    public Candy Candy;
    void OnEnable()
    {
        _candyRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (Candy.Reference == CandyReference.General)
        {
            int index = Random.Range(0, Candy.GeneralSprite.Length - 1);
            _spriteRenderer.sprite = Candy.GeneralSprite[index];
        }
        else
            _spriteRenderer.sprite = Candy.SpecialSprite;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 unitVector = (mousePos - (Vector2)transform.position).normalized;
        _candyRigidbody.velocity = Candy.ThrowSpeed * unitVector;

        float rotateSpeed = Random.Range(10.0f, 270.0f);
        _candyRigidbody.angularVelocity = rotateSpeed;

        Invoke(nameof(DestroyProjectile), _lifeTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == false && collision.TryGetComponent(out HasHealth damageable))
        {
            damageable.TakeDamage(Candy.DamageDone);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
