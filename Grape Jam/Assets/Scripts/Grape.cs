﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour {
    public float groundMoveForce;
    public float airMoveForce;
    public Vector3 forceDirX;
    public Vector3 forceDirZ;

    float _curMoveForce;
    Vector3 _appliedForce;
    float _maxMoveSpeed = 15f;
    Rigidbody _rigidbody;
    public Rigidbody Rigidbody {
        get { return _rigidbody; }
    }

    Transform swarmCenter;
    float expandSpeed = 12f;

    bool _canJump;
    float _groundDrag = 0f;
    float _groundAngularDrag = 5f;
    float _airDrag = 3f;
    float _airAngularDrag = 1f;

    float _jumpSquatTime = 0.05f;
    float _jumpSquatTimer = 0f;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        DetermineForce();

        _rigidbody.AddForce(_appliedForce);

        if(!_canJump) {
            _jumpSquatTimer += Time.deltaTime;
        }

        _appliedForce = Vector3.zero;
	}

    void DetermineForce() {
        _appliedForce += forceDirX * (_curMoveForce * Input.GetAxis("Vertical"));
        _appliedForce += forceDirZ * (_curMoveForce * Input.GetAxis("Horizontal"));

        // Don't add force if we've exceeded the max speed
        if(Mathf.Abs(_rigidbody.velocity.x) > _maxMoveSpeed && 
            Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(_appliedForce.x)) {
            _appliedForce.x = 0;
        }
        if (Mathf.Abs(_rigidbody.velocity.z) > _maxMoveSpeed &&
            Mathf.Sign(_rigidbody.velocity.z) == Mathf.Sign(_appliedForce.z)) {
            _appliedForce.z = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        CheckFloor(collision);
    }

    private void OnCollisionStay(Collision collision) {
        CheckFloor(collision);
    }

    void CheckFloor(Collision collision) {
        if ((collision.collider.tag == "Ground" || collision.collider.tag == "Grape") &&
            _jumpSquatTimer > _jumpSquatTime) {
            // Make sure we collided from the bottom
            Vector3 closestPoint = collision.collider.ClosestPoint(transform.position);
            if (transform.position.y - closestPoint.y > 0.15f) {
                // We've hit the floor
                _canJump = true;
                _curMoveForce = groundMoveForce;
                _rigidbody.angularDrag = _groundAngularDrag;
            }
        }
    }

    public void TryJump(float jumpForce) {
        if(_canJump) {
            _appliedForce.y = jumpForce;
            _curMoveForce = airMoveForce;
            _rigidbody.angularDrag = _airAngularDrag;
            _canJump = false;
            _jumpSquatTimer = 0f;
        }
    }

    public void Expand() {
        Vector3 dir = (transform.position - swarmCenter.position).normalized;
        dir.y = 0;
        _appliedForce += dir * expandSpeed;
    }

    public void Contract() {
        Vector3 dir = (swarmCenter.position - transform.position).normalized;
        dir.y = 0;
        _appliedForce += dir * expandSpeed;
    }
}
