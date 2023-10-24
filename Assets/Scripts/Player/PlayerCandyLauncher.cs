using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCandyLauncher : MonoBehaviour
{
    [Header("Candy Launcher")]
    [SerializeField] GameObject _candyObject;
    [SerializeField] int _maxAmmo;

    int _currentAmmo;
    List<GameObject> _magazine;
    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
    public event Action OnAmmoChanged;

    public static PlayerCandyLauncher Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _magazine = new List<GameObject>();
        for (int i = 0; i < _maxAmmo; i++)
            _magazine.Add(_candyObject);
    }

    void Start()
    {
        _currentAmmo = _maxAmmo;
        OnAmmoChanged?.Invoke();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UIPauseMenu.GameIsPaused == false)
            Fire();
    }

    void Fire()
    {
        if (_currentAmmo == 0)
            return;
        _currentAmmo--;
        Instantiate(_magazine[0], transform.position, Quaternion.identity);
        OnAmmoChanged?.Invoke();
        _magazine.RemoveAt(0);
    }

    public void AddAmmo(int amount)
    {
        for (int i = 0; i < amount; i++)
            _magazine.Add(_candyObject);
        _currentAmmo += amount;
        OnAmmoChanged?.Invoke();
    }

    public float GetHealValue()
    {
        Candy candy = _magazine[0].GetComponent<CandyProjectile>().Candy;
        if (candy != null)
        {
            _magazine.RemoveAt(0);
            return candy.HealthGiven;
        }
        else
            return 0.0f;
    }

}
