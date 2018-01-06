using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisTutorial : MonoBehaviour {

    public Text textUI;
    public string controllerTutorialText;
    public string keyboardTutorialText;
    public string axisName;

    bool _finished;
    bool _usingController;

    // Use this for initialization
    void Start() {
        textUI.gameObject.SetActive(false);
        _finished = false;

        if (Input.GetJoystickNames().Length > 0) {
            _usingController = true;
        } else {
            _usingController = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis(axisName) > 0 || Input.GetMouseButtonDown(0) && textUI.gameObject.activeSelf && !_finished) {
            textUI.gameObject.SetActive(false);
            _finished = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape" && !_finished) {
            if (_usingController) {
                textUI.text = controllerTutorialText;
            } else {
                textUI.text = keyboardTutorialText;
            }
            textUI.gameObject.SetActive(true);
        }
    }
}