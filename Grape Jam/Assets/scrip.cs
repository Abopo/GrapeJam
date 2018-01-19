using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = (new Vector3(0.0f, -50.0f, 0.0f));
	}
}
