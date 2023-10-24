using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Gradient _nightCycle;
    [SerializeField] float _cycleRealDuration;
    [SerializeField] float _cycleGameDuration;

    Light2D _moonLight;
    float _realTime;
    float _rawTime;
    float _cycleFactor;
    int _cycleGameMin;
    int _cycleGameHour;
    float _initialLightIntensity;
    HasHealth _playerHealth;

    public float CycleGameMin => _cycleGameMin;
    public float CycleGameHour => _cycleGameHour;

    public static GameManager Instance;
    public static bool Ended = false;
    public event Action OnWin;
    public event Action OnLose;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ResetValues();
        _moonLight = GameObject.FindGameObjectWithTag("Moonlight").GetComponent<Light2D>();
        _playerHealth.OnDeath += Lose;
        AudioManager.Instance.Play("Rain");
        _cycleFactor = _cycleGameDuration / _cycleRealDuration;
        _initialLightIntensity = _moonLight.intensity;
    }

    private void ResetValues()
    {
        Ended = false;
        _cycleGameMin = 0;
        _cycleGameHour = 0;
        _rawTime = 0.0f;
        _realTime = 0.0f;
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HasHealth>();
        _playerHealth.OnDeath -= Lose;
    }

    void Update()
    {
        if (Ended == true)
            return;
        _realTime += Time.deltaTime;
        _rawTime += Time.deltaTime * _cycleFactor;
        _cycleGameHour = (int)_rawTime / 360;
        _cycleGameMin = ((int)_rawTime / 6) - (_cycleGameHour * 60);
        _moonLight.color = _nightCycle.Evaluate(_realTime / _cycleRealDuration);
        _moonLight.intensity = _initialLightIntensity + (_realTime / _cycleRealDuration);
        if (_realTime >= _cycleRealDuration)
            Win();
    }

    public void Win()
    {
        AudioManager.Instance.Stop("Rain");
        Ended = true;
        OnWin?.Invoke();
        Time.timeScale = 0.0f;
    }

    public void Lose()
    {
        AudioManager.Instance.Stop("Rain");
        Ended = true;
        OnLose?.Invoke();
        Time.timeScale = 0.0f;
    }

    public void Restart()
    {
        ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        AudioManager.Instance.Play("Rain");
    }
}
