using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
    public string nextLevel;

    GrapeSwarm _swarm;
    int _counter;

    float _time = 8.0f;
    float _timer = 0f;

	// Use this for initialization
	void Start () {
        _swarm = GameObject.FindGameObjectWithTag("GrapeSwarm").GetComponent<GrapeSwarm>();
        _counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(_counter >= _swarm.GetGrapeCount()) {
            _timer += Time.deltaTime;
            if (_timer >= _time) {
                EndLevel();
            }
        }
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Grape") {
            _counter++;
        }
    }

    void EndLevel() {
        SceneManager.LoadScene(nextLevel);
    }
}
