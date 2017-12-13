﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public Camera depthCamera;

	// Use this for initialization
	void Start () {
        depthCamera = GameObject.FindGameObjectWithTag("Depth Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(depthCamera.transform.position, depthCamera.transform.up);
	}
}
