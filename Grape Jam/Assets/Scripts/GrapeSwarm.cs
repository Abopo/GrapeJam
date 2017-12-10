using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSwarm : MonoBehaviour {
    public float moveForce = 10;
    public float jumpForce = 500;

    Grape[] _grapes;
    Transform _camera;

    Vector3 _appliedForce;
    Vector3 _rotateAxis;
    float _rotateSpeed = 50f;
    bool _tryJump;

    // Use this for initialization
    void Start () {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Grape");
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        _grapes = new Grape[gs.Length];
        int index = 0;
        foreach(GameObject g in gs) {
            _grapes[index] = g.GetComponent<Grape>();
            ++index;
        }

        _tryJump = false;
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();

        CommandSwarm();

		FollowSwarm();

        _appliedForce = Vector3.zero;
        _rotateAxis = Vector3.zero;
    }

    void CheckInput() {
        if (Input.GetKey(KeyCode.W)) {
            // Move forward
            //_appliedForce.z = moveForce;
            _appliedForce += (transform.position - _camera.position).normalized * moveForce;
        }
        if (Input.GetKey(KeyCode.S)) {
            //_appliedForce.z = -moveForce;
            _appliedForce += (transform.position - _camera.position).normalized * -moveForce;
        }
        if (Input.GetKey(KeyCode.D)) {
            //_appliedForce.x = moveForce;
            _appliedForce += _camera.right * moveForce;
        }
        if (Input.GetKey(KeyCode.A)) {
            //_appliedForce.x = -moveForce;
            _appliedForce += _camera.right * -moveForce;
        }
        if (Input.GetKey(KeyCode.Q)) {
            _rotateAxis.y = -1f;
        }
        if (Input.GetKey(KeyCode.E)) {
            _rotateAxis.y = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            // Jump
            _appliedForce.y = jumpForce;
            _tryJump = false;
        }
    }

    void CommandSwarm() {
        foreach (Grape g in _grapes) {
            g.appliedForce = _appliedForce;
            g.transform.RotateAround(transform.position, _rotateAxis, _rotateSpeed * Time.deltaTime);

            if (_tryJump) {
                g.TryJump(jumpForce);
            }
        }
    }

    void FollowSwarm() {
        Vector3 totalPosition = Vector3.zero;
        foreach(Grape grape in _grapes) {
            totalPosition += grape.transform.position;
        }

        Vector3 averagePosition = totalPosition / _grapes.Length;

        transform.position = averagePosition;
    }
}
