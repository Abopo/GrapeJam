using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSwitch : MonoBehaviour {

    enum Direction
    {
        Clockwise,
        Counterclockwise
    }

    [SerializeField] GameObject ObjectToModify = null;
    [SerializeField] float NumberOfRotations = 1;
    //[SerializeField] Direction SpinDirection = Direction.Clockwise;
    [SerializeField] float Speed = 5;

    int numGrapesActing = 0;


    float totalForce = 0;

   // float _rotationValue = 0;
    float _initialYRotation;

	// Use this for initialization
	void Start () {
        _initialYRotation = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, totalForce * Time.deltaTime * Speed);
        Debug.Log(totalForce);


        numGrapesActing = 0;
        totalForce = 0;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > 0)
        {
            numGrapesActing++;
            totalForce += collision.impulse.magnitude;

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.impulse.magnitude > 0)
        {
            numGrapesActing++;
            totalForce += collision.impulse.magnitude;

        }
    }
}
