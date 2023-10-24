using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] _sounds;
    [SerializeField] AudioMixerGroup _masterMixer;
    [SerializeField] AudioMixerGroup _musicMixer;
    [SerializeField] AudioMixerGroup _sfxMixer;

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        foreach (Sound sound in _sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            switch (sound.Mixer)
            {
                case "Master":
                    sound.Source.outputAudioMixerGroup = _masterMixer;
                    break;
                case "Music":
                    sound.Source.outputAudioMixerGroup = _musicMixer;
                    break;
                case "SFX":
                    sound.Source.outputAudioMixerGroup = _sfxMixer;
                    break;
                default:
                    sound.Source.outputAudioMixerGroup = _masterMixer;
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioMixer mainMixer = _masterMixer.audioMixer;
        mainMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0.0f));
        mainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0.0f));
        mainMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0.0f));
        Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_sounds, t => t.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {name}, not found!");
            return;
        }
        sound.Source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(_sounds, t => t.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {name}, not found!");
            return;
        }
        sound.Source.Stop();
    }
}
