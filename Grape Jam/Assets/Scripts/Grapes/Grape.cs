using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour {
    public float groundMoveForce;
    public float airMoveForce;
    public Vector3 forceDirX;
    public Vector3 forceDirZ;

    float _curMoveForce;
    Vector3 _appliedForce;
    float _maxMoveSpeed = 15f;
    Rigidbody _rigidbody;
    public Rigidbody Rigidbody {
        get { return _rigidbody; }
    }

    Transform _swarmCenter;
    float _expandSpeed = 12f;

    bool _canJump;
    float _groundDrag = 0f;
    float _groundAngularDrag = 5f;
    float _airDrag = 3f;
    float _airAngularDrag = 1f;

    float _jumpSquatTime = 0.05f;
    float _jumpSquatTimer = 0f;

    bool _onSlide = false;
    bool _takeInput = true;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
	}

    // Update is called once per frame
    void LateUpdate() {
        if (!_onSlide && _takeInput) {
            DetermineForce();

            _rigidbody.AddForce(_appliedForce);
        }

        if(!_canJump) {
            _jumpSquatTimer += Time.deltaTime;
        }

        _appliedForce = Vector3.zero;
	}

    void DetermineForce() {
        _appliedForce += forceDirX * (_curMoveForce * Input.GetAxis("Vertical"));
        _appliedForce += forceDirZ * (_curMoveForce * Input.GetAxis("Horizontal"));

        // Don't add force if we've exceeded the max speed
        if(Mathf.Abs(_rigidbody.velocity.x) > _maxMoveSpeed && 
            Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(_appliedForce.x)) {
            _appliedForce.x = 0;
        }
        if (Mathf.Abs(_rigidbody.velocity.z) > _maxMoveSpeed &&
            Mathf.Sign(_rigidbody.velocity.z) == Mathf.Sign(_appliedForce.z)) {
            _appliedForce.z = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        CheckFloor(collision);

        if (collision.collider.tag == "Trampoline") {
            _canJump = true;
            TryJump(collision.collider.GetComponent<Trampoline>().bounceForce);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Death Plane") {
            Die();
        }
    }    

    private void OnCollisionStay(Collision collision) {
        CheckFloor(collision);

        if(collision.collider.tag == "Slide") {
            // Don't apply any movement forces while in a slide
            _onSlide = true;
        }
    }

    void CheckFloor(Collision collision) {
        if ((collision.collider.tag == "Ground" || collision.collider.tag == "Grape" || collision.collider.tag == "Slide") &&
            _jumpSquatTimer > _jumpSquatTime) {
            // Make sure we collided from the bottom
            Vector3 closestPoint = collision.collider.ClosestPoint(transform.position);
            if (transform.position.y - closestPoint.y > 0.15f) {
                // We've hit the floor
                _canJump = true;
                _curMoveForce = groundMoveForce;
                _rigidbody.angularDrag = _groundAngularDrag;
                _onSlide = false;
            }
        }
    }

    public void TryJump(float jumpForce) {
        if(_canJump) {
            _appliedForce.y = jumpForce;
            _curMoveForce = airMoveForce;
            _rigidbody.angularDrag = _airAngularDrag;
            _canJump = false;
            _jumpSquatTimer = 0f;
            _onSlide = false;
        }
    }

    public void Orbit(float dir) {
        Vector3 towardCenter = (_swarmCenter.position - transform.position).normalized;
        Vector3 right = Vector3.Cross(towardCenter, Vector3.up);
        _rigidbody.AddForce(right * (_curMoveForce/2) * dir);
        // Force is increased based on distance to center point to keep orbit consistent
        float inwardForce = _curMoveForce;
        float centerDist = (_swarmCenter.position - transform.position).magnitude;
        if (centerDist > 1) {
            inwardForce = inwardForce / centerDist;
        }
        _rigidbody.AddForce(towardCenter * inwardForce);
    }

    public void Expand() {
        Vector3 dir = (transform.position - _swarmCenter.position).normalized;
        dir.y = 0;
        _appliedForce += dir * _expandSpeed;
    }

    public void Contract() {
        Vector3 dir = (_swarmCenter.position - transform.position).normalized;
        dir.y = 0;
        _appliedForce += dir * _expandSpeed;
    }

    public void Trim() {
        // Remove self from swarm
        _swarmCenter.GetComponent<GrapeSwarm>().LoseGrape(this);

        // Stop taking Input
        _takeInput = false;
    }

    public void JoinSwarm() {
        // Remove self from swarm
        _swarmCenter.GetComponent<GrapeSwarm>().AddGrape(this);

        // Stop taking Input
        _takeInput = true;
    }

    public void Die() {
        // Remove self from swarm
        _swarmCenter.GetComponent<GrapeSwarm>().LoseGrape(this);

        // Destroy this grape
        DestroyObject(this.gameObject);
    }
}
