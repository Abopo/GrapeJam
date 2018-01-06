using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrimTutorial : MonoBehaviour {
    public Text textUI;
    string trimTutorialText = "B to Trim";
    string trimTutorialText2 = "C to Trim";
    string addTutorialText = "B to Join";
    string addTutorialText2 = "C to Join";

    bool _finished1, _finished2;
    bool _usingController;

    PressurePlate _pressurePlate;
    GrapeSwarm _grapeSwarm;

	// Use this for initialization
	void Start () {
        _pressurePlate = GetComponent<PressurePlate>();
        _grapeSwarm = GameObject.FindGameObjectWithTag("GrapeSwarm").GetComponent<GrapeSwarm>();

        textUI.gameObject.SetActive(false);
        _finished1 = false;
        _finished2 = false;

        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
            _usingController = true;
        } else {
            _usingController = false;
        }
    }

    // Update is called once per frame
    void Update () {
	    if(_pressurePlate.CurrentlyColliding.Count > 0) {
            if (GrapeIsOut() && !_finished1) {
                //textUI.text = trimTutorialText;
                if (_usingController) {
                    textUI.text = trimTutorialText;
                } else {
                    textUI.text = trimTutorialText2;
                }
                textUI.gameObject.SetActive(true);
            } else if(_finished1 && GrapeIsBackIn() && !_finished2) {
                //textUI.text = addTutorialText;
                if (_usingController) {
                    textUI.text = addTutorialText;
                } else {
                    textUI.text = addTutorialText2;
                }
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
