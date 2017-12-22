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

    float upYPos;
    float downYPos;

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

        upYPos = transform.parent.position.y;
        downYPos = upYPos - 0.15f;
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

        if(_currentlyColliding.Count >= requiredGrapes && transform.parent.position.y > downYPos) {
            // Move down
            transform.parent.Translate(0f, -0.5f * Time.deltaTime, 0f, Space.World);
        } else if (_currentlyColliding.Count < requiredGrapes && transform.parent.position.y < upYPos) {
            // Move up
            transform.parent.Translate(0f, 0.5f * Time.deltaTime, 0f, Space.World);
        } else if(_currentlyColliding.Count >= requiredGrapes && transform.parent.position.y <= downYPos && !Active) {
            // Activate
            ObjectToToggle.Activate();
            Active = true;
        }
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
        if (_currentlyColliding.Count >= requiredGrapes && !Active) {
            _meshRenderer.material = _activeMaterial;
        }
    }

    void RemoveGrape(GameObject grape) {
        _currentlyColliding.Remove(grape);
        if (_currentlyColliding.Count < requiredGrapes) {
            _meshRenderer.material = _unactiveMaterial;
            ObjectToToggle.Deactivate();
            Active = false;
        }
    }
}
