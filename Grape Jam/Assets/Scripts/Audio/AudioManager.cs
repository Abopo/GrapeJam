using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioSource BGM = null;
    private AudioSource[] SFX;

    float _masterVolume = 1.0f;
    float _bgmVolume = 1.0f;
    float _sfxVolume = 1.0f;

	// Use this for initialization
	void Start () {
        SFX = FindObjectsOfType<AudioSource>();

        PlayerPrefs.SetFloat("SFXVolume", 0.25f);

        SetVolume();
	}
	
    void SetVolume()
    {
        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        _bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);

        foreach(var effects in SFX)
        {
            effects.volume = _sfxVolume * _masterVolume;
        }

        BGM.volume = _bgmVolume * _masterVolume;
    }
}