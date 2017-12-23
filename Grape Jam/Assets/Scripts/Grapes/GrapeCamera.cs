using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeCamera : MonoBehaviour {
    Transform _grapeSwarm;

    Vector3 _rotateAxis;
    float _rotateSpeed = 50f;
    float _speedOffset = 0f;
    int _xInverted = 1;
    int _yInverted = 1;


    float _moveSpeed = 10f;
    float _minHeight = 4.5f;
    float _maxHeight = 20f;

    bool _usingController = false;

    Vector3 dampVel = Vector3.zero;

    // Use this for initialization
    void Start () {

        _xInverted = PlayerPrefs.GetInt("XInverted", 1);
        _yInverted = PlayerPrefs.GetInt("YInverted", 1);
        _rotateSpeed = PlayerPrefs.GetFloat("CameraSpeed", 50);


        _grapeSwarm = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;

        _rotateAxis = Vector3.zero;

        if (Input.GetJoystickNames().Length > 0) {
            _usingController = true;
        }

        AttachToSwarm();
	}

    void AttachToSwarm() {
        transform.position = _grapeSwarm.position;
        transform.Translate(0f, 5f, -15f, Space.World);
    }

    // Update is called once per frame
    void Update () {
        CheckInput();

        // Invert horizontal rotation
        _rotateAxis.y *= _xInverted;

        transform.RotateAround(_grapeSwarm.transform.position, _rotateAxis, (_rotateSpeed * _speedOffset) * Time.deltaTime);
        AdjustDistance();

        if (transform.localPosition.y > _maxHeight) {
            transform.localPosition = new Vector3(transform.localPosition.x, _maxHeight, transform.localPosition.z);
        } else if (transform.localPosition.y < _minHeight) {
            transform.localPosition = new Vector3(transform.localPosition.x, _minHeight, transform.localPosition.z);
        }

        transform.LookAt(_grapeSwarm);

        _rotateAxis = Vector3.zero;
    }
    
    void CheckInput() {
        if (_usingController) {
            _rotateAxis.y = Input.GetAxis("CameraHorizontal");
            _speedOffset = Mathf.Abs(Input.GetAxis("CameraHorizontal"));
            if (Input.GetAxis("CameraVertical") != 0) {
                transform.Translate(0f, (_moveSpeed * Input.GetAxis("CameraVertical")) * Time.deltaTime * _yInverted, 0f, Space.World);
            }

            if (Input.GetAxis("CameraZoom") > 0) {
                Vector3 dir = (_grapeSwarm.position - transform.position).normalized;
                transform.Translate(transform.forward * 5f * Time.deltaTime, Space.World);
            }
            if(Input.GetAxis("CameraZoom") < 0) {
                Vector3 dir = (_grapeSwarm.position - transform.position).normalized;
                transform.Translate(transform.forward * -5f * Time.deltaTime, Space.World);
            }
        } else {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                _rotateAxis.y = 1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                _rotateAxis.y = -1.0f;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(0f, _moveSpeed * Time.deltaTime, 0f, Space.World);
                if (transform.localPosition.y > _maxHeight) {
                    transform.localPosition = new Vector3(transform.localPosition.x, _maxHeight, transform.localPosition.z);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(0f, -_moveSpeed * Time.deltaTime, 0f, Space.World);
                if (transform.localPosition.y < _minHeight) {
                    transform.localPosition = new Vector3(transform.localPosition.x, _minHeight, transform.localPosition.z);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)) {
            transform.LookAt(_grapeSwarm);
        }
    }

    // Zooms in/out based on spread of swarm
    void AdjustDistance() {
        float furthestGrape = _grapeSwarm.GetComponent<GrapeSwarm>().FurthestGrapeDistance();
        if(furthestGrape <= 1) {
            furthestGrape = 1f;
        }
        _maxHeight = furthestGrape * 10f;
        if(_maxHeight < 20f) {
            _maxHeight = 20f;
        }
        float wantDistance = 2f * furthestGrape;
        float curDist = (transform.position - _grapeSwarm.transform.position).magnitude;

        if(wantDistance < 15f) {
            wantDistance = 15f;
        }

        if (Mathf.Abs(Mathf.Abs(curDist) - Mathf.Abs(wantDistance)) > 1f) {
            Vector3 dir = (_grapeSwarm.position - transform.position).normalized;
            Vector3 wantPos = _grapeSwarm.position - dir * wantDistance;
            transform.position = Vector3.SmoothDamp(transform.position, wantPos, ref dampVel, 0.3f);
        }
    }

    public void SetXInversion(bool invert)
    {
        if (invert)
            _xInverted = -1;
        else
            _xInverted = 1;

    }

    public void SetYInversion(bool invert)
    {
        if (invert)
            _yInverted = -1;
        else
            _yInverted = 1;
    }

    public void SetCameraSpeed(float speed)
    {
        _rotateSpeed = speed;
    }
}
