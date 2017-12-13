using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] ActivateableObject ObjectToToggle = null;

    List<GameObject> _currentlyColliding;
    List<GameObject> _deadGrapes;

    // Use this for initialization
    void Start () {
        _currentlyColliding = new List<GameObject>();
        _deadGrapes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_currentlyColliding.Count == 0)
            ObjectToToggle.Deactivate();

        // Failsafe for scenarios where objects within the list are deactivated 
        // while currently colliding with the trigger, and thus not calling OnTriggerExit

        foreach (GameObject grape in _currentlyColliding) {
            
            // This might break
            if (grape == null)
                _deadGrapes.Add(grape);
            else if (grape.activeSelf == false)
                _deadGrapes.Add(grape);
        }

        foreach(GameObject deadGrape in _deadGrapes)
        {
            _currentlyColliding.Remove(deadGrape);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape") {
            _currentlyColliding.Add((other.gameObject));
            ObjectToToggle.Activate();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Grape")
            _currentlyColliding.Remove(other.gameObject);
    }
}
