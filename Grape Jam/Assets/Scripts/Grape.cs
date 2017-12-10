﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour {
    public Vector3 appliedForce;

    Rigidbody _rigidbody;
    public Rigidbody Rigidbody {
        get { return _rigidbody; }
    }

    Transform swarmCenter;
    float expandSpeed = 12f;

    bool _canJump;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
	}
	
	// Update is called once per frame
	void Update () {
        _rigidbody.AddForce(appliedForce);

        appliedForce = Vector3.zero;
	}

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Ground") {
            _canJump = true;
            _rigidbody.drag = 0f;
            _rigidbody.angularDrag = 5f;
        }
    }

    public void TryJump(float jumpForce) {
        if(_canJump) {
            appliedForce.y = jumpForce;
            _rigidbody.drag = 2f;
            _rigidbody.angularDrag = 0f;
            _canJump = false;
        }
    }

    public void Expand() {
        Vector3 dir = (transform.position - swarmCenter.position).normalized;
        appliedForce += dir * expandSpeed;
    }

    public void Contract() {
        Vector3 dir = (swarmCenter.position - transform.position).normalized;
        appliedForce += dir * expandSpeed;
    }
}
