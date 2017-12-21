using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
    public string nextLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Grape") {
            // End the level
            EndLevel();
        }
    }

    void EndLevel() {
        SceneManager.LoadScene(nextLevel);
    }
}
