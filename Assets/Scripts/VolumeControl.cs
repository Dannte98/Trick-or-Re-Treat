using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] string _parameter;
    [SerializeField] Slider _slider;

    // Start is called before the first frame update
    void Awake() => _slider.onValueChanged.AddListener(SetVolume);

    void Start() => _slider.value = PlayerPrefs.GetFloat(_parameter, _slider.value);

    void SetVolume(float volume) => _audioMixer.SetFloat(_parameter, volume);

    void OnDisable() => PlayerPrefs.SetFloat(_parameter, _slider.value);
}
