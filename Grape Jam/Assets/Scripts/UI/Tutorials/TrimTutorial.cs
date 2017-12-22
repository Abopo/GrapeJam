using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrimTutorial : MonoBehaviour {
    public Text textUI;
    string trimTutorialText = "B to Trim";
    string addTutorialText = "B to Join";

    bool _finished1, _finished2;

    PressurePlate _pressurePlate;
    GrapeSwarm _grapeSwarm;

	// Use this for initialization
	void Start () {
        _pressurePlate = GetComponent<PressurePlate>();
        _grapeSwarm = GameObject.FindGameObjectWithTag("GrapeSwarm").GetComponent<GrapeSwarm>();

        textUI.gameObject.SetActive(false);
        _finished1 = false;
        _finished2 = false;
    }

    // Update is called once per frame
    void Update () {
	    if(_pressurePlate.CurrentlyColliding.Count > 0) {
            if (GrapeIsOut() && !_finished1) {
                textUI.text = trimTutorialText;
                textUI.gameObject.SetActive(true);
            } else if(_finished1 && GrapeIsBackIn() && !_finished2) {
                textUI.text = addTutorialText;
                textUI.gameObject.SetActive(true);
            }
        }

        if (Input.GetButtonDown("Trim") && textUI.gameObject.activeSelf) {
            if (!_finished1) {
                textUI.gameObject.SetActive(false);
                _finished1 = true;
            } else if(_finished1 && !_finished2) {
                textUI.gameObject.SetActive(false);
                _finished2 = true;
            }
        }
    }

    bool GrapeIsOut() {
        float mag = (_pressurePlate.CurrentlyColliding[0].transform.position - _grapeSwarm.transform.position).magnitude;
        if (mag > _grapeSwarm.AverageDistance()+2.5f) {
            return true;
        }

        return false;
    }

    bool GrapeIsBackIn() {
        float mag = (_pressurePlate.CurrentlyColliding[0].transform.position - _grapeSwarm.transform.position).magnitude;
        if (mag < _grapeSwarm.AverageDistance() + 2.5f) {
            return true;
        }

        return false;
    }
}
