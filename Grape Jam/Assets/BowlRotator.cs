using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlRotator : MonoBehaviour {

    [SerializeField] float XCycleDuration = 10.0f;
    [SerializeField] float ZCycleCuration = 8.0f;
    [SerializeField] Quaternion _targetRotation = new Quaternion(0.1f, 0.0f, 0.1f, 1.0f);

    float _xElapsedTime = 0.0f;
    float _zElapsedTime = 0.0f;

    Transform _transform;

    // Use this for initialization
    void Start () {
        _transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        _xElapsedTime += Time.deltaTime;
        _zElapsedTime += Time.deltaTime;

        if (_xElapsedTime > XCycleDuration)
        {
            _xElapsedTime = 0;
            _targetRotation.x *= -1;
        }

        if(_zElapsedTime > ZCycleCuration)
        {
            _zElapsedTime = 0.0f;
            _targetRotation.z *= -1;
        }

        _transform.rotation =  Quaternion.Slerp(_transform.rotation, _targetRotation, _xElapsedTime / XCycleDuration * Time.deltaTime);
	}
}