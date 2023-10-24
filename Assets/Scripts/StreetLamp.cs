using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StreetLamp : MonoBehaviour
{
    [SerializeField] float _minFlickTime;
    [SerializeField] float _maxFlickTime;
    [SerializeField] float _damagedTime;

    [SerializeField] Sprite _damagedSprite;    

    Collider2D _lightCollider;
    SpriteRenderer _spriteRenderer;
    Light2D _light;

    ParticleSystem _smokeParticles;


    float _flickerTime;
    float _flickerTimer;
    float _damagedTimer;

    bool _isBeneath;
    bool _isFlickering;
    bool _isDamaged;

    void Start()
    {
        _flickerTime = Random.Range(_minFlickTime, _maxFlickTime);
        _lightCollider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light = GetComponentInChildren<Light2D>();
        _smokeParticles = GetComponentInChildren<ParticleSystem>();

    }

    void Update()
    {
        if(_isBeneath == true)
        {
            _flickerTimer += Time.deltaTime;
            _damagedTimer += Time.deltaTime;
        }

        if(_flickerTimer >= _flickerTime && _isFlickering == false)
        {
            InvokeRepeating(nameof(Flicker), 0.0f, 1.0f);
            _isFlickering = true;
        }

        if(_damagedTimer >= _damagedTime && _isDamaged == false)
        {
            CancelInvoke();
            _light.enabled = false;
            AudioManager.Instance.Play("DamagedLamp");
            _spriteRenderer.sprite = _damagedSprite;
            _lightCollider.enabled = false;
            _smokeParticles.Play();
            _isDamaged = true;
        }
    }

    void Flicker() => _light.enabled = !_light.enabled;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
            _isBeneath = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
            _isBeneath = false;
    }
}
