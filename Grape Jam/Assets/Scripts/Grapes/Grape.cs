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
    AudioSource _audioSource;

    Transform _swarmCenter;
    float _expandSpeed = 12f;

    bool _canJump;
    bool _justJumped;
    float _groundAngularDrag = 5f;
    float _airAngularDrag = 1f;

    float _jumpTime = 0.25f;
    float _jumpTimer = 0f;

    bool _leftGround = false;
    float _fallCheckTime = 0.1f;
    float _fallCheckTimer = 0f;

    bool _onSlide = false;
    bool _takeInput = true;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
        _curMoveForce = airMoveForce;

        _audioSource.volume = PlayerPrefs.GetFloat("MasterVolume", 1.0f) * PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    // Update is called once per frame
    void LateUpdate() {
        if (!_onSlide && _takeInput) {
            DetermineForce();

            _rigidbody.AddForce(_appliedForce);
        }

        UpdateTimers();

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

    void UpdateTimers() {
        // Keeps track of when this grape can have it's jump canceled
        if (!_canJump) {
            _jumpTimer += Time.deltaTime;
            if (_jumpTimer >= _jumpTime) {
                _justJumped = false;
            }
        }

        // Give small buffer after falling off a platform to jump
        // (also allows jumping during small bounce off floor when landing)
        if (_leftGround) {
            _fallCheckTimer += Time.deltaTime;
            if (_fallCheckTimer >= _fallCheckTime) {
                _canJump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        CheckFloor(collision);

        if (collision.collider.tag == "Trampoline") {
            TrampolineBounce(collision.collider.GetComponent<Trampoline>().bounceForce);
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

    private void OnCollisionExit(Collision collision) {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Slide") {
            _leftGround = true;
            _fallCheckTimer = 0f;
        }
    }

    void CheckFloor(Collision collision) {
        if ((collision.collider.tag == "Ground" || collision.collider.tag == "Grape" || collision.collider.tag == "Slide") && 
            _jumpTimer >= _jumpTime) {
            // Make sure we collided from the bottom
            foreach (ContactPoint cp in collision.contacts) {
                if (transform.position.y - cp.point.y > 0.15f) {
                    // We've hit the floor
                    _canJump = true;
                    _justJumped = false;
                    _curMoveForce = groundMoveForce;
                    _rigidbody.angularDrag = _groundAngularDrag;
                    _onSlide = false;
                    _leftGround = false;
                }
            }
        }
    }

    public void TryJump(float jumpForce) {
        if(_canJump && !_onSlide) {
            _appliedForce.y = jumpForce;
            _curMoveForce = airMoveForce;
            _rigidbody.angularDrag = _airAngularDrag;
            _canJump = false;
            _jumpTimer = 0f;
            _justJumped = true;
            _audioSource.Play();
        }
    }

    public void JumpCancel(float jumpForce) {
        if(_justJumped) {
            _appliedForce.y = -jumpForce / 2;
            _justJumped = false;
        }
    }

    void TrampolineBounce(float bounceForce) {
        _appliedForce.y = bounceForce;
        _curMoveForce = airMoveForce;
        _rigidbody.angularDrag = _airAngularDrag;
        _canJump = false;
        _justJumped = false;
        _jumpTimer = 0f;
        // TODO: make trampoline sound
        _audioSource.Play();
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

    public void CutOrbit() {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x / 4, _rigidbody.velocity.y, _rigidbody.velocity.z / 4);
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

    public void SetTakeInput(bool takeInput)
    {
        _takeInput = takeInput;
    }
}
