using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : ActivateableObject {

    Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Activate() {
        _rigidbody.isKinematic = false;
    }

    public override void Deactivate() {
        //_rigidbody.isKinematic = true;
    }

    public override void Toggle() {
    }

}
