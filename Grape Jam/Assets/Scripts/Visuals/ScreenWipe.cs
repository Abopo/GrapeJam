using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenWipe : MonoBehaviour {
    public static ScreenWipe instance;

    ParticleSystem _particleSystem;
    Transform _mainCamera;
    AudioSource _audioSource;

    float _transitionTime = 0.35f;
    float _transitionTimer = 0f;
    bool _transtitioning = false;
    string levelName;

    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        _particleSystem = GetComponent<ParticleSystem>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += SceneLoaded;
	}
	
	// Update is called once per frame
	void Update () {
        FollowCamera();

		if(_transtitioning) {
            _transitionTimer += Time.deltaTime;
            if(_transitionTimer >= _transitionTime) {
                // Load the next scene
                if(levelName != null) {
                    SceneManager.LoadScene(levelName);
                    _transtitioning = false;
                }
            }
        }
	}

    void FollowCamera() {
        transform.position = _mainCamera.position;
        transform.Translate(6f, 0.8f, -5f);
        transform.rotation = _mainCamera.rotation;
        transform.Rotate(0f, -90f, 0f);
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode) {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        FollowCamera();
    }

    public void Wipe() {
        _particleSystem.Play();
        _transtitioning = true;
    }

    public void Wipe(string level) {
        _particleSystem.Play();
        _audioSource.Play();
        _transtitioning = true;
        _transitionTimer = 0f;
        levelName = level;
    }
}
