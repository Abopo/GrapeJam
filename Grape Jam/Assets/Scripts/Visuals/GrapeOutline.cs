using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeOutline : MonoBehaviour {
    public LayerMask rayMask;

    Transform _cameraTransform;
    Ray _toCamera;
    SpriteRenderer _spriteRenderer;

    bool _enabled = true;

	// Use this for initialization
	void Start () {
        _cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _toCamera = new Ray();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!_enabled) {
            return;
        }

        _toCamera.origin = transform.position;
        Vector3 toCam = _cameraTransform.position - transform.position;
        _toCamera.direction = toCam.normalized;
        RaycastHit rayHit;

        if(Physics.Raycast(_toCamera, out rayHit, toCam.magnitude, rayMask)) {
            _spriteRenderer.enabled = true;
            Debug.DrawRay(_toCamera.origin, _toCamera.direction * toCam.magnitude);
        } else {
            _spriteRenderer.enabled = false;
        }
	}

    public void Disable() {
        _spriteRenderer.enabled = false;
        _enabled = false;
    }
}
