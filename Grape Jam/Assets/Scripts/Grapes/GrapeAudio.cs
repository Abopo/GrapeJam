using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeAudio : MonoBehaviour {
    AudioManager _audioManager;
    AudioSource _audioSource;

    AudioClip[] _jumpClips;
    AudioClip _pipeClip;
    AudioClip _deathClip;
    AudioClip[] _jamClips;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioManager = (AudioManager)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();

        _audioManager.AddAudioSource(_audioSource);

        _jumpClips = Resources.LoadAll<AudioClip>("Audio/SFX/JumpSounds");
        _pipeClip = Resources.Load<AudioClip>("Audio/SFX/pipe");
        _deathClip = Resources.Load<AudioClip>("Audio/SFX/Pop1");
        _jamClips = Resources.LoadAll<AudioClip>("Audio/SFX/JamSounds");
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayJump() {
        if (!_audioSource.isPlaying) {
            int r = Random.Range(0, _jumpClips.Length);
            _audioSource.clip = _jumpClips[r];
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

    public void PlayJamSound() {
        if(!_audioSource.isPlaying) {
            int r = Random.Range(0, _jamClips.Length);
            _audioSource.clip = _jamClips[r];
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
