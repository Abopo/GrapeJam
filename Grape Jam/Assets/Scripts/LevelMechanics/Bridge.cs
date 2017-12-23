using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : ActivateableObject {

    Rigidbody _rigidbody;
    Vector3 _gravity;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
         _rigidbody.AddForce(_gravity);
	}

    public override bool Activate() {
        _rigidbody.isKinematic = false;
        _gravity = Physics.gravity;
        return true;
    }

    public override bool Deactivate() {
        _gravity = -Physics.gravity;
        return false;
    }

    public override void Toggle() {
    }
}
