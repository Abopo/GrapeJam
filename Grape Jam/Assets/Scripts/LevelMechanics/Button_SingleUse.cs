using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SingleUse : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] ActivateableObject ObjectToActivate = null;
    [SerializeField] int requiredGrapes = 1;

    List<GameObject> _currentlyColliding;
    List<GameObject> _deadGrapes;

    AudioSource _audioSource;

    // Use this for initialization
    void Start() {
        _currentlyColliding = new List<GameObject>();
        _deadGrapes = new List<GameObject>();

        _audioSource = GetComponent<AudioSource>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>().AddAudioSource(_audioSource);
    }

    // Update is called once per frame
    void Update() {
        // Failsafe for scenarios where objects within the list are deactivated 
        // while currently colliding with the trigger, and thus not calling OnTriggerExit

        foreach (GameObject grape in _currentlyColliding) {
            // This might break
            if (grape == null)
                _deadGrapes.Add(grape);
            else if (grape.activeSelf == false)
                _deadGrapes.Add(grape);
        }

        foreach (GameObject deadGrape in _deadGrapes) {
            RemoveGrape(deadGrape);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (ObjectToActivate == null)
            return;

        if (other.tag == "Grape" && !Active) {
            if (other.tag == "Grape") {
                AddGrape(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Grape") {
            RemoveGrape(other.gameObject);
        }
    }

    void AddGrape(GameObject grape) {
        _currentlyColliding.Add(grape);
        if (_currentlyColliding.Count >= requiredGrapes && !Active) {
            Active = true;
            ObjectToActivate.Activate();
            //TODO: Swap Between On/Off visually
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            // Play audio clip
            _audioSource.Play();
        }
    }

    void RemoveGrape(GameObject grape) {
        _currentlyColliding.Remove(grape);
    }
}
