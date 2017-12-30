using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] ActivateableObject ObjectToToggle = null;
    [SerializeField] int requiredGrapes = 1;

    List<GameObject> _currentlyColliding;
    List<GameObject> _deadGrapes;

    MeshRenderer _meshRenderer;
    Material _unactiveMaterial;
    Material _activeMaterial;

    float _upYPos;
    float _downYPos;
    bool _done = false;

    public List<GameObject> CurrentlyColliding {
        get { return _currentlyColliding; }
    }

    // Use this for initialization
    void Start () {
        _currentlyColliding = new List<GameObject>();
        _deadGrapes = new List<GameObject>();

        _meshRenderer = transform.parent.GetComponent<MeshRenderer>();
        _unactiveMaterial = Resources.Load<Material>("Blender/Materials/PressurePlate");
        _activeMaterial = Resources.Load<Material>("Blender/Materials/PressurePlate2");

        _upYPos = transform.parent.position.y;
        _downYPos = _upYPos - 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
        // Failsafe for scenarios where objects within the list are deactivated 
        // while currently colliding with the trigger, and thus not calling OnTriggerExit

        foreach (GameObject grape in _currentlyColliding) {
            
            // This might break
            if (grape == null)
                _deadGrapes.Add(grape);
            else if (grape.activeSelf == false)
                _deadGrapes.Add(grape);
        }

        foreach(GameObject deadGrape in _deadGrapes) {
            RemoveGrape(deadGrape);
        }

        /*
        if(_currentlyColliding.Count >= requiredGrapes && transform.parent.position.y > _downYPos) {
            // Move down
            transform.parent.Translate(0f, -0.5f * Time.deltaTime, 0f, Space.World);
        } else if (_currentlyColliding.Count < requiredGrapes && transform.parent.position.y < _upYPos && !_done) {
            // Move up
            transform.parent.Translate(0f, 0.5f * Time.deltaTime, 0f, Space.World);
        } else if(_currentlyColliding.Count >= requiredGrapes && transform.parent.position.y <= _downYPos && !Active) {
            // Activate
            _done = ObjectToToggle.Activate();
            Active = true;
        }
        */
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape") {
            AddGrape(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Grape") {
            RemoveGrape(other.gameObject);
        }
    }

    void AddGrape(GameObject grape) {
        _currentlyColliding.Add(grape);
        CheckGrapeCount();
    }

    void RemoveGrape(GameObject grape) {
        _currentlyColliding.Remove(grape);
        CheckGrapeCount();
    }

    void CheckGrapeCount() {
        if (_currentlyColliding.Count >= requiredGrapes && !Active) {
            _done = ObjectToToggle.Activate();
            Active = true;
            transform.parent.Translate(0f, -0.001f, 0f, Space.World);
            _meshRenderer.material = _activeMaterial;
        } else  if (_currentlyColliding.Count < requiredGrapes && Active) {
            _done = ObjectToToggle.Deactivate();
            Active = false;
            transform.parent.Translate(0f, 0.001f, 0f, Space.World);
            _meshRenderer.material = _unactiveMaterial;
        }
    }
}
