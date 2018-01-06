using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractTutorial : ActivateableObject {

    public Text textUI;
    public string controllerTutorialText;
    public string keyboardTutorialText;
    public string axisName;

    bool _finished;

    bool _activated = false;
    float _time = 2.0f;
    float _timer = 0f;

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
        if(_activated && !_finished) {
            _timer += Time.deltaTime;
            if(_timer > _time) {
                if (_usingController) {
                    textUI.text = controllerTutorialText;
                } else {
                    textUI.text = keyboardTutorialText;
                }
                textUI.gameObject.SetActive(true);
            }
        }
        if (Input.GetAxis(axisName) > 0 || Input.GetMouseButtonDown(1) && textUI.gameObject.activeSelf && !_finished) {
            textUI.gameObject.SetActive(false);
            _finished = true;
        }
    }

    public override bool Activate() {
        _activated = true;
        return true;
    }

    public override bool Deactivate() {
        return false;
    }
}
