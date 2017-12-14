using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSwarm : MonoBehaviour {
    public float groundMoveForce;
    public float airMoveForce;
    public float jumpForce = 500;

    List<Grape> _grapes = new List<Grape>();
    Transform _camera;

    Vector3 _forceDirX;
    Vector3 _forceDirZ;
    Vector3 _rotateAxis;
    float _rotateSpeed = 75f;
    bool _tryJump;
    bool _expand;
    bool _contract;

    bool _usingController = false;

    // Use this for initialization
    void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        InitializeGrapes();

        _tryJump = false;
        _expand = false;
        _contract = false;

        if(Input.GetJoystickNames().Length > 0) {
            _usingController = true;
        }
	}

    void InitializeGrapes() {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Grape");
        int index = 0;
        foreach (GameObject g in gs) {
            _grapes.Add(g.GetComponent<Grape>());
            _grapes[index].groundMoveForce = groundMoveForce;
            _grapes[index].airMoveForce = airMoveForce;
            ++index;
        }
    }

    // Update is called once per frame
    void Update () {
        CheckInput();

        CommandSwarm();

		FollowSwarm();

        _forceDirX = Vector3.zero;
        _forceDirZ = Vector3.zero;
        _rotateAxis = Vector3.zero;
    }

    void CheckInput() {
        Vector3 horComPos = new Vector3(_camera.position.x, transform.position.y, _camera.position.z);

        if (_usingController) {
            _forceDirZ += (transform.position - horComPos).normalized;
            _forceDirX += _camera.right;
        } else {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
                // Move forward
                _forceDirZ += (transform.position - horComPos).normalized;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) {
                _forceDirX += _camera.right;
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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) {
            // Jump
            //_appliedForce.y = jumpForce;
            _tryJump = true;
        }
    }

    void CommandSwarm() {
        foreach (Grape g in _grapes) {
            g.forceDirZ = _forceDirX;
            g.forceDirX = _forceDirZ;
            if(_rotateAxis.y > 0) {
                g.Orbit(1);
            } else if(_rotateAxis.y < 0) {
                g.Orbit(-1);
            }
            //g.transform.RotateAround(transform.position, _rotateAxis, _rotateSpeed * Time.deltaTime);

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

        Vector3 averagePosition = totalPosition / _grapes.Count;

        transform.position = averagePosition;
    }

    public float FurthestGrapeDistance() {
        float furthest = 0;

        float tempMag = 0;
        foreach(Grape g in _grapes) {
            tempMag = (transform.position - g.transform.position).magnitude;
            if (tempMag > furthest) {
                furthest = tempMag;
            }
        }

        return furthest;
    }

    public void LoseGrape(Grape grape) {
        _grapes.Remove(grape);
        FollowSwarm();
    }

    public void AddGrape(Grape grape) {
        _grapes.Add(grape);
        grape.airMoveForce = airMoveForce;
        grape.groundMoveForce = groundMoveForce;
        FollowSwarm();
    }

    public int GetGrapeCount() {
        return _grapes.Count;
    }
}
