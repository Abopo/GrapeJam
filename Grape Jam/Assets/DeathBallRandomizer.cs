using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallRandomizer : MonoBehaviour {

    [SerializeField] float Interval = -1.0f;
    [SerializeField] float ForceScalar = 10.0f;

    Rigidbody _rigidbody;
    Quaternion _rotation;
    Vector3 _velocity;
    float _elapsedTime;

	void Start () {

        _rigidbody = GetComponent<Rigidbody>();

        if(Interval == -1.0f)
            Interval = 5.0f + Random.value * 10.0f;   
	}

    void Update() {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > Interval) {
            _rotation = Random.rotation;// * 10.0f;
            _velocity = Vector3.zero;
            _velocity.x = _rotation.x * 10.0f;
            _velocity.z = _rotation.z * 10.0f;

            _rigidbody.velocity += _velocity;

            _elapsedTime = 0.0f;
        }
        Debug.Log(_rigidbody.velocity);
    }
}