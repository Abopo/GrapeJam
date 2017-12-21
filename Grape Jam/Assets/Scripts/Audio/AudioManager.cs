using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioSource BGM = null;
    public List<AudioSource> SFX = null;

    float _masterVolume = 1.0f;
    float _bgmVolume = 1.0f;
    float _sfxVolume = 1.0f;

	// Using Awake to make sure SFX is populated before SetVolume can be called when using volume sliders
	void Awake () {
        SFX = new List<AudioSource>();
        SFX.AddRange(FindObjectsOfType<AudioSource>());
        SetVolume();
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
            effects.volume = _sfxVolume * _masterVolume;
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
}