using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour {
    public Vector3 appliedForce;

    Rigidbody _rigidbody;
    public Rigidbody Rigidbody {
        get { return _rigidbody; }
    }

    Transform swarmCenter;
    float expandSpeed = 12f;

    bool _canJump;
    float _jumpSquatTime = 0.25f;
    float _jumpSquatTimer = 0f;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
	}
	
	// Update is called once per frame
	void Update () {
        _rigidbody.AddForce(appliedForce);

        if(!_canJump) {
            _jumpSquatTimer += Time.deltaTime;
        }

        appliedForce = Vector3.zero;
	}

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Ground" && _jumpSquatTimer > _jumpSquatTime) {
            // Make sure we collided from the bottom
            Vector3 closestPoint = collision.collider.ClosestPoint(transform.position);
            if (closestPoint.y < transform.position.y) {
                // We've hit the floor
                _canJump = true;
                _rigidbody.drag = 0f;
                _rigidbody.angularDrag = 5f;
            }
        }
    }

    public void TryJump(float jumpForce) {
        if(_canJump) {
            appliedForce.y = jumpForce;
            _rigidbody.drag = 2f;
            _rigidbody.angularDrag = 0f;
            _canJump = false;
            _jumpSquatTimer = 0f;
        }
    }

    public void Expand() {
        Vector3 dir = (transform.position - swarmCenter.position).normalized;
        dir.y = 0;
        appliedForce += dir * expandSpeed;
    }

    public void Contract() {
        Vector3 dir = (swarmCenter.position - transform.position).normalized;
        dir.y = 0;
        appliedForce += dir * expandSpeed;
    }
}
