using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeAudio : MonoBehaviour {
    AudioManager _audioManager;
    AudioSource _audioSource;

    AudioClip _jumpClip;
    AudioClip _pipeClip;
    AudioClip _deathClip;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioManager = (AudioManager)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();

        _audioManager.AddAudioSource(_audioSource);

        _jumpClip = Resources.Load<AudioClip>("Audio/SFX/Jump10");
        _pipeClip = Resources.Load<AudioClip>("Audio/SFX/pipe");
        _deathClip = Resources.Load<AudioClip>("Audio/SFX/Pop1");
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayJump() {
        if (!_audioSource.isPlaying) {
            _audioSource.clip = _jumpClip;
            _audioSource.Play();
        }
    }

    public void PlayPipe() {
        if (!_audioSource.isPlaying) {
            _audioSource.clip = _pipeClip;
            _audioSource.Play();
        }
    }

    public void PlayDeathSound() {
        if (!_audioSource.isPlaying) {
            _audioSource.clip = _deathClip;
            _audioSource.Play();
        }
    }

    public bool IsPlaying() {
        return _audioSource.isPlaying;
    }

    public void Remove() {
        _audioManager.RemoveAudioSource(_audioSource);
    }
}
