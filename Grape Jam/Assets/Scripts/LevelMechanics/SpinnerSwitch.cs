using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSwitch : MonoBehaviour {

    enum Direction
    {
        Clockwise,
        Counterclockwise
    }

    [SerializeField] GameObject ObjectToRotate = null;
    [SerializeField] Vector3 Axis = new Vector3(0, 1, 0);
    [SerializeField] float Speed = 1;

    Vector3 _initialRotation_Spinner;
    Vector3 _initialRotation_Object;
    float _currentRotation;

	// Use this for initialization
	void Start () {
        _initialRotation_Spinner = transform.rotation.eulerAngles;
        _initialRotation_Object = ObjectToRotate.transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        if (ObjectToRotate == null)
            return;

        _currentRotation = transform.rotation.eulerAngles.y - _initialRotation_Spinner.y;
        Quaternion newRotation = Quaternion.Euler(_initialRotation_Object.x + (_currentRotation * Speed * Axis.x), 
                                                  _initialRotation_Object.y + (_currentRotation * Speed * Axis.y), 
                                                  _initialRotation_Object.x + (_currentRotation * Speed * Axis.z));
        ObjectToRotate.transform.rotation = newRotation;
	}
}