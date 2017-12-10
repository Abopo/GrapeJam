using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeCamera : MonoBehaviour {
    Transform grapeSwarm;

    Vector3 rotateAxis;
    float rotateSpeed = 50f;

    float _moveSpeed = 10f;
    float _minHeight = 1f;
    float _maxHeight = 7f;

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
            transform.Translate(0f, _moveSpeed * Time.deltaTime, 0f, Space.World);
            if(transform.localPosition.y > _maxHeight) {
                transform.localPosition = new Vector3(transform.localPosition.x, _maxHeight, transform.localPosition.z);
            }
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0f, -_moveSpeed * Time.deltaTime, 0f, Space.World);
            if (transform.localPosition.y < _minHeight) {
                transform.localPosition = new Vector3(transform.localPosition.x, _minHeight, transform.localPosition.z);
            }
        }
    }
}
