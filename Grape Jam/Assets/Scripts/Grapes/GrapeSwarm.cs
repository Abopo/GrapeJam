using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSwarm : MonoBehaviour {
    public float groundMoveForce;
    public float airMoveForce;
    public float jumpForce = 500;

    public GrapeSpawner respawnPoint;

    List<Grape> _grapes = new List<Grape>();
    Transform _camera;

    Vector3 _wantPos;
    Vector3 _smoothDamp;

    Vector3 _forceDirX;
    Vector3 _forceDirZ;
    Vector3 _rotateAxis;
    bool _tryJump;
    bool _jumpCancel;
    bool _endRotate;
    bool _expand;
    bool _contract;

    float _spawnTime = 0.8f;
    float _spawnTimer = 0;

    bool _usingController = false;

    public List<Grape> Grapes {
        get { return _grapes; }
    }

    // Use this for initialization
    void Start () {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        InitializeGrapes();
        if (_grapes.Count == 0) {
            _spawnTimer = 0f;
        }

        _tryJump = false;
        _jumpCancel = false;
        _endRotate = false;
        _expand = false;
        _contract = false;

        if(LevelManager.UsingController()) {
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
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= _spawnTime && _grapes.Count == 0) {
            respawnPoint.RespawnSwarm();
        }

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
        } else if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("RotateLeft")) {
            // cancel rotation momentum
            _endRotate = true;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetButton("RotateRight")) {
            _rotateAxis.y = 1f;
        } else if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("RotateRight")) {
            // cancel rotation momentum
            _endRotate = true;
        }
        if (Input.GetMouseButton(0) || (Input.GetAxis("Expand") > 0)) {
            // Expand the swarm
            _expand = true;
        }
        if (Input.GetMouseButton(1) || (Input.GetAxis("Contract") > 0)) {
            // Contract the swarm
            _contract = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) {
            // Jump
            //_appliedForce.y = jumpForce;
            _tryJump = true;
        } else if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump")) {
            // Cancel jump momentum
            _jumpCancel = true;
        }

        if(Input.GetButtonDown("Trim")) {
            // Cut away grapes outside of range
            TrimGrapes();
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
            if(_endRotate) {
                g.CutOrbit();
            }
            //g.transform.RotateAround(transform.position, _rotateAxis, _rotateSpeed * Time.deltaTime);

            if (_tryJump) {
                g.TryJump(jumpForce);
            }
            if(_jumpCancel) {
                g.JumpCancel(jumpForce);
            }
            if(_expand) {
                g.Expand();
            }
            if (_contract) {
                g.Contract();
            }
        }

        _tryJump = false;
        _jumpCancel = false;
        _endRotate = false;
        _expand = false;
        _contract = false;
    }

    void FollowSwarm() {
        if(_grapes.Count == 0) {
            return;
        }

        Vector3 totalPosition = Vector3.zero;
        foreach(Grape grape in _grapes) {
            totalPosition += grape.transform.position;
        }

        Vector3 averagePosition = totalPosition / _grapes.Count;

        _wantPos = averagePosition;

        float mag = (_wantPos - transform.position).magnitude;
        if (mag > 1f) {
            // Move towards the desired position
            transform.position = Vector3.SmoothDamp(transform.position, _wantPos, ref _smoothDamp, 0.15f);
        } else {
            transform.position = _wantPos;
        }
    }

    void TrimGrapes() {
        float averageDistance = AverageDistance();
        averageDistance += 2.5f;
        float tempMag = 0;
        List<Grape> cutGrapes = new List<Grape>();
        // Get the grapes to be cut
        foreach (Grape g in _grapes) {
            tempMag = (transform.position - g.transform.position).magnitude;
            if(tempMag > averageDistance) {
                cutGrapes.Add(g);
            }
        }
        // Cut those grapes out 
        // (This process takes both loops becuase you can't remove from the same list you are iterating through)
        foreach(Grape g in cutGrapes) {
            g.Trim();
        }

        // Add in any grapes that are within the ring
        GameObject[] _allGrapes = GameObject.FindGameObjectsWithTag("Grape");
        foreach(GameObject g in _allGrapes) {
            tempMag = (transform.position - g.transform.position).magnitude;
            if (tempMag < averageDistance && !_grapes.Contains(g.GetComponent<Grape>())) {
                g.GetComponent<Grape>().JoinSwarm();
            }
        }
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

    public float AverageDistance() {
        if(_grapes.Count <= 1) {
            return 2f;
        }

        float averageDis = 0;

        foreach (Grape g in _grapes) {
            averageDis += (transform.position - g.transform.position).magnitude;
        }

        averageDis = averageDis / _grapes.Count;


        return averageDis;
    }

    public void LoseGrape(Grape grape) {
        _grapes.Remove(grape);
        grape.SetTakeInput(false);
        if(_grapes.Count <= 0) {
           // respawnPoint.RespawnSwarm();
            _spawnTimer = 0f;
        }
    }

    public void AddGrape(Grape grape) {
        _grapes.Add(grape);
        grape.airMoveForce = airMoveForce;
        grape.groundMoveForce = groundMoveForce;
    }

    public int GetGrapeCount() {
        return _grapes.Count;
    }
}
