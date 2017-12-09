using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeCamera : MonoBehaviour {
    Transform grapeSwarm;

    Vector3 rotateAxis;
    float rotateSpeed = 50f;

	// Use this for initialization
	void Start () {
        grapeSwarm = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;

        rotateAxis = Vector3.zero;

        AttachToSwarm();
	}

    void AttachToSwarm() {
        transform.position = grapeSwarm.position;
        transform.Translate(0f, 5f, -15f, Space.World);
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();

        transform.RotateAround(grapeSwarm.transform.position, rotateAxis, rotateSpeed * Time.deltaTime);
        transform.LookAt(grapeSwarm);
        AttachToSwarm();
        rotateAxis = Vector3.zero;
    }

    void CheckInput() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            rotateAxis.y = 1.0f;
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            rotateAxis.y = -1.0f;
        }
        if(Input.GetKey(KeyCode.UpArrow)) {
            rotateAxis.x = 1.0f;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            rotateAxis.x = -1.0f;
        }
    }
}
