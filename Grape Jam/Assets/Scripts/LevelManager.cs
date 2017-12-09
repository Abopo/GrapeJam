using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0f, Physics.gravity.y * 2.0f, 0f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
