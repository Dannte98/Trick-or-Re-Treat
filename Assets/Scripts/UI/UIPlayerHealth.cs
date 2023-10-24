using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] Image _healthImage;
    [SerializeField] Gradient _healthColor;
    HasHealth _playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HasHealth>();
        _playerHealth.OnHealthChanged += UpdateHealthUI;
    }

    void UpdateHealthUI()
    {
        float healthValue = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        _healthSlider.value = healthValue;
        _healthImage.color = _healthColor.Evaluate(healthValue);
    }
}
