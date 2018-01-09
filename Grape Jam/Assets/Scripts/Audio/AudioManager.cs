using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioSource BGM = null;
    public List<AudioSource> SFX = null;

    float _masterVolume = 1.0f;
    float _bgmVolume = 1.0f;
    float _sfxVolume = 1.0f;

    AudioSource _audioSource;
    AudioClip _menuMusic;
    AudioClip _levelMusic;

	// Using Awake to make sure SFX is populated before SetVolume can be called when using volume sliders
	void Awake () {
        SFX = new List<AudioSource>();
        SFX.AddRange(FindObjectsOfType<AudioSource>());
        SetVolume();

        _audioSource = GetComponent<AudioSource>();
        _menuMusic = Resources.Load<AudioClip>("Audio/BGM/Happy Alley");
        _levelMusic = Resources.Load<AudioClip>("Audio/BGM/Chipper Doodle v2");

        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }
	
    public void SetVolume()
    {
        if(BGM == null || SFX == null)
            return;
    
        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        _bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);

        foreach(var effects in SFX)
        {
            if (effects != null) {
                effects.volume = _sfxVolume * _masterVolume;
            }
        }

        BGM.volume = _bgmVolume * _masterVolume;
    }

    public void AddAudioSource(AudioSource source)
    {
        if (!SFX.Contains(source)) {
            SFX.Add(source);
            SetVolume();
        }
    }

    public void AddAudioSource(AudioSource[] source)
    {
        SFX.AddRange(source);
        SetVolume();
    }

    public void RemoveAudioSource(AudioSource source) {
        SFX.Remove(source);
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex < 3 && _audioSource != null) {
            // Play menu music
            if(_audioSource.clip != _menuMusic) {
                _audioSource.clip = _menuMusic;
                _audioSource.Play();
            }
        } else if(scene.buildIndex > 2 && _audioSource != null) {
            // Play level music
            if (_audioSource.clip != _levelMusic) {
                _audioSource.clip = _levelMusic;
            }
            _audioSource.Play();
        }
    }
}