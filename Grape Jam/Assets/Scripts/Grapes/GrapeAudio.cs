using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeAudio : MonoBehaviour {
    AudioManager _audioManager;
    AudioSource _audioSource;

    AudioClip _jumpClip;
    AudioClip _pipeClip;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioManager = (AudioManager)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();

        _audioManager.AddAudioSource(_audioSource);

        _jumpClip = Resources.Load<AudioClip>("Audio/Jump10");
        _pipeClip = Resources.Load<AudioClip>("Audio/pipe");
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayJump() {
        _audioSource.clip = _jumpClip;
        _audioSource.Play();
    }

    public void PlayPipe() {
        _audioSource.clip = _pipeClip;
        _audioSource.Play();
    }

    public void Remove() {
        _audioManager.RemoveAudioSource(_audioSource);
    }
}
