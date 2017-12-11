using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSwarm : MonoBehaviour {
    public float moveForce = 15;
    public float jumpForce = 500;

    Grape[] _grapes;
    Transform _camera;

    Vector3 _appliedForce;
    Vector3 _rotateAxis;
    float _rotateSpeed = 75f;
    bool _tryJump;
    bool _expand;
    bool _contract;

    bool _usingController = false;

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
        _expand = false;
        _contract = false;

        if(Input.GetJoystickNames().Length > 0) {
            _usingController = true;
        }
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
        Vector3 horComPos = new Vector3(_camera.position.x, transform.position.y, _camera.position.z);

        if (_usingController) {
            _appliedForce += (transform.position - horComPos).normalized * (moveForce * Input.GetAxis("Vertical"));
            _appliedForce += _camera.right * (moveForce * Input.GetAxis("Horizontal"));
        } else {
            if (Input.GetKey(KeyCode.W)) {
                // Move forward
                //_appliedForce.z = moveForce;
                _appliedForce += (transform.position - horComPos).normalized * moveForce;
            }
            if (Input.GetKey(KeyCode.S)) {
                //_appliedForce.z = -moveForce;
                _appliedForce += (transform.position - horComPos).normalized * -moveForce;
            }
            if (Input.GetKey(KeyCode.D)) {
                //_appliedForce.x = moveForce;
                _appliedForce += _camera.right * moveForce;
            }
            if (Input.GetKey(KeyCode.A)) {
                //_appliedForce.x = -moveForce;
                _appliedForce += _camera.right * -moveForce;
            }
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetButton("RotateLeft")) {
            // Rotate the swarm
            _rotateAxis.y = -1f;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetButton("RotateRight")) {
            _rotateAxis.y = 1f;
        }
        if (Input.GetKey(KeyCode.R) || (Input.GetAxis("Expand") > 0)) {
            // Expand the swarm
            _expand = true;
        }
        if (Input.GetKey(KeyCode.F) || (Input.GetAxis("Contract") > 0)) {
            // Contract the swarm
            _contract = true;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")) {
            // Jump
            //_appliedForce.y = jumpForce;
            _tryJump = true;
        }        
    }

    void CommandSwarm() {
        foreach (Grape g in _grapes) {
            g.appliedForce = _appliedForce;
            g.transform.RotateAround(transform.position, _rotateAxis, _rotateSpeed * Time.deltaTime);

            if (_tryJump) {
                g.TryJump(jumpForce);
            }
            if(_expand) {
                g.Expand();
            }
            if (_contract) {
                g.Contract();
            }
        }

        _tryJump = false;
        _expand = false;
        _contract = false;
    }

    void FollowSwarm() {
        Vector3 totalPosition = Vector3.zero;
        foreach(Grape grape in _grapes) {
            totalPosition += grape.transform.position;
        }

        Vector3 averagePosition = totalPosition / _grapes.Length;

        transform.position = averagePosition;
    }

    public float AverageSpread() {
        float averageSpread = 0;
       
        foreach(Grape g in _grapes) {
            averageSpread += (transform.position - g.transform.position).magnitude;
        }

        averageSpread = averageSpread / _grapes.Length;

        return averageSpread;
    }
}
