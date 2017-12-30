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
    float _groundMaxMoveSpeed = 15f;
    float _airMaxMoveSpeed = 10f;
    float _curMaxMoveSpeed = 15f;
    Rigidbody _rigidbody;
    public Rigidbody Rigidbody {
        get { return _rigidbody; }
    }
    GrapeAudio _grapeAudio;

    Transform _swarmCenter;

    bool _canJump;
    bool _justJumped;
    float _groundAngularDrag = 5f;
    float _airAngularDrag = 1f;

    float _jumpTime = 0.25f;
    float _jumpTimer = 0f;

    bool _leftGround = true;
    float _fallCheckTime = 0.1f;
    float _fallCheckTimer = 0f;

    bool _onSlide = false;
    bool _takeInput = true;
    bool _initialSpawn = true;


    bool _jumpingIntoJar = false;
    Transform _jar;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _grapeAudio = GetComponent<GrapeAudio>();
        _swarmCenter = GameObject.FindGameObjectWithTag("GrapeSwarm").transform;
        _curMoveForce = airMoveForce;

        _grapeAudio.PlayPipe();
    }

    // Update is called once per frame
    void LateUpdate() {
        if (!_onSlide && _takeInput) {
            if (!_initialSpawn) {
                DetermineForce();
            }

            _rigidbody.AddForce(_appliedForce);
        } else if(_jumpingIntoJar) {
            Vector3 force = Vector3.zero;
            Vector3 toJar = _jar.position - transform.position;
            toJar.Normalize();
            //force.x = toJar.x * (toJar.magnitude * 20f);
            //force.z = toJar.z * (toJar.magnitude * 20f);

            _rigidbody.AddForce(force);
            _rigidbody.velocity = new Vector3(toJar.x * toJar.magnitude * 10f, _rigidbody.velocity.y, toJar.z * toJar.magnitude * 10f);
        }

        UpdateTimers();

        _appliedForce = Vector3.zero;
	}

    void DetermineForce() {
        _appliedForce += forceDirX * (_curMoveForce * Input.GetAxis("Vertical"));
        _appliedForce += forceDirZ * (_curMoveForce * Input.GetAxis("Horizontal"));
        
        // Don't add force if we've exceeded the max speed
        if(Mathf.Abs(_rigidbody.velocity.x) > _curMaxMoveSpeed && 
            Mathf.Sign(_rigidbody.velocity.x) == Mathf.Sign(_appliedForce.x)) {
            _appliedForce.x = 0;
        }
        if (Mathf.Abs(_rigidbody.velocity.z) > _curMaxMoveSpeed &&
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
        if(other.tag == "LevelEnd") {
            _jumpingIntoJar = false;
            _takeInput = false;
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
            _onSlide = false;
            _fallCheckTimer = 0f;
            _curMoveForce = airMoveForce;
            _curMaxMoveSpeed = _airMaxMoveSpeed;
            _rigidbody.angularDrag = _airAngularDrag;
        }
    }

    void CheckFloor(Collision collision) {
        if (collision.collider.tag == "Ground") {
            _initialSpawn = false;
        }

        if ((collision.collider.tag == "Ground" || collision.collider.tag == "Grape" || collision.collider.tag == "Slide") && 
            _jumpTimer >= _jumpTime) {
            // Make sure we collided from the bottom
            foreach (ContactPoint cp in collision.contacts) {
                if (transform.position.y - cp.point.y > 0.15f) {
                    // We've hit the floor
                    _canJump = true;
                    _justJumped = false;
                    _curMoveForce = groundMoveForce;
                    _curMaxMoveSpeed = _groundMaxMoveSpeed;
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
            _curMaxMoveSpeed = _airMaxMoveSpeed;
            _rigidbody.angularDrag = _airAngularDrag;
            _canJump = false;
            _jumpTimer = 0f;
            _justJumped = true;
            _grapeAudio.PlayJump();
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
        _curMaxMoveSpeed = _airMaxMoveSpeed;
        _rigidbody.angularDrag = _airAngularDrag;
        _canJump = false;
        _justJumped = false;
        _jumpTimer = 0f;
        // TODO: make trampoline sound
        _grapeAudio.PlayJump();
    }

    public void Orbit(float dir) {
        if (_onSlide || _initialSpawn) {
            return;
        }

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
        if (_onSlide || _initialSpawn) {
            return;
        }

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x / 4, _rigidbody.velocity.y, _rigidbody.velocity.z / 4);
    }

    public void Expand() {
        if (_onSlide || _initialSpawn) {
            return;
        }

        Vector3 dir = (transform.position - _swarmCenter.position).normalized;
        dir.y = 0;
        _appliedForce += dir * _curMoveForce/1.5f;
    }

    public void Contract() {
        if (_onSlide || _initialSpawn) {
            return;
        }

        Vector3 dir = (_swarmCenter.position - transform.position).normalized;
        dir.y = 0;
        _appliedForce += dir * _curMoveForce/1.5f;
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

    public void Shoot() {
        Vector3 shootForce;
        shootForce.x = Random.Range(75f, 100f);
        shootForce.x *= Mathf.Sign(Random.Range(-1, 1));
        shootForce.y = Random.Range(800f, 900f);
        shootForce.z = Random.Range(75f, 100f);
        shootForce.z *= Mathf.Sign(Random.Range(-1, 1));

        if (_rigidbody == null) {
            _rigidbody = GetComponent<Rigidbody>();
        }

        _rigidbody.AddForce(shootForce);
    }

    public void Die() {
        // Remove self from swarm
        _swarmCenter.GetComponent<GrapeSwarm>().LoseGrape(this);

        _grapeAudio.Remove();

        // Destroy this grape
        DestroyObject(this.gameObject);
    }

    public void SetTakeInput(bool takeInput) {
        _takeInput = takeInput;
    }

    public void JumpIntoJar(Transform inJar) {
        if(_jumpingIntoJar) {
            return;
        }

        Vector3 force = Vector3.zero;
        _jar = inJar;
        Vector3 toJar = _jar.position - transform.position;
        toJar.Normalize();
        //force.x = toJar.x * (toJar.magnitude * 20f);
        //force.z = toJar.z * (toJar.magnitude * 20f);
        force.y = 900f;

        _rigidbody.AddForce(force);
        _rigidbody.velocity = new Vector3(toJar.x * toJar.magnitude, _rigidbody.velocity.y, toJar.z * toJar.magnitude);

        _grapeAudio.PlayJump();

        _takeInput = false;
        _jumpingIntoJar = true;
    }
}
