using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
    GrapeSwarm _swarm;
    LevelManager _levelManager;
    int _counter;

    float _time = 5.0f;
    float _timer = 0f;

	// Use this for initialization
	void Start () {
        _swarm = GameObject.FindGameObjectWithTag("GrapeSwarm").GetComponent<GrapeSwarm>();
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        _counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(_counter >= _swarm.GetGrapeCount()) {
            _timer += Time.deltaTime;
            if (_timer >= _time) {
                _levelManager.LevelEnd();
            }
        }
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Grape") {
            _counter++;
        }
    }
}
