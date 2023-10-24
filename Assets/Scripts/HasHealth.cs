using System;
using UnityEngine;

public abstract class HasHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float _maxHealth;
    protected float _currentHealth;
    protected bool _isDead;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;

    public event Action OnHealthChanged;
    public event Action OnDeath;

    void Awake() => _currentHealth = _maxHealth;

    protected virtual void Start() => OnHealthChanged?.Invoke();

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke();
        if (_currentHealth <= 0.0f)
            Death();
    }

    protected bool Heal(float amount)
    {
        if (_currentHealth + amount <= _maxHealth)
        {
            _currentHealth += amount;
            OnHealthChanged?.Invoke();
            return true;
        }
        else
            return false;
    }

    public void Death()
    {
        _isDead = true;
        OnDeath?.Invoke();
    }
}
