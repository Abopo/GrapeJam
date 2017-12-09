using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSwarm : MonoBehaviour {
    public float moveForce = 10;
    public float jumpForce = 500;

    Grape[] _grapes;
    Transform _camera;

    Vector3 _appliedForce;
    bool _canJump;

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
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();

        CommandSwarm();

		FollowSwarm();

        _appliedForce = Vector3.zero;
    }

    void CheckInput() {
        if (Input.GetKey(KeyCode.W)) {
            // Move forward
            //_appliedForce.z = moveForce;
            _appliedForce = (transform.position - _camera.position).normalized * moveForce;
        }
        if (Input.GetKey(KeyCode.S)) {
            _appliedForce.z = -moveForce;
        }
        if (Input.GetKey(KeyCode.D)) {
            _appliedForce.x = moveForce;
        }
        if (Input.GetKey(KeyCode.A)) {
            _appliedForce.x = -moveForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canJump) {
            // Jump
            _appliedForce.y = jumpForce;
            _canJump = false;
        }
    }

    void CommandSwarm() {
        foreach (Grape g in _grapes) {
            g.appliedForce = _appliedForce;

            g.Rigidbody.drag = 2f;
            g.Rigidbody.angularDrag = 0f;
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
