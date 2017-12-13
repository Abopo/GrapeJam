using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnstyle : MonoBehaviour {

    [SerializeField] float RotationSpeed = 1.0f;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
	}
}
