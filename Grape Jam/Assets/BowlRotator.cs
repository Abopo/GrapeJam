using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlRotator : MonoBehaviour {


    [SerializeField] float XCycleDuration = 10.0f;
    [SerializeField] float ZCycleCuration = 8.0f;

    float _xElapsedTime = 0.0f;
    float _zElapsedTime = 0.0f;

    Transform _transform;
    Quaternion _initialRotation;
    Quaternion _targetRotation = new Quaternion(0.125f, 0.0f, 0.125f, 1.0f);

    // Use this for initialization
    void Start () {
        _transform = GetComponent<Transform>();
        _initialRotation = _transform.rotation;
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


        Debug.Log(_targetRotation);

        Debug.Log(string.Format("Progress: {0}", _xElapsedTime / XCycleDuration));

        _transform.rotation =  Quaternion.Slerp(_transform.rotation, _targetRotation, _xElapsedTime / XCycleDuration * Time.deltaTime);
	}


    private void DeathReset()
    {
        _transform.rotation = _initialRotation;
    }
}
