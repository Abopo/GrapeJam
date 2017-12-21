using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractTutorial : ActivateableObject {

    public Text textUI;
    public string tutorialText;
    public string axisName;

    bool _finished;

    bool _activated = false;
    float _time = 2.0f;
    float _timer = 0f;

    // Use this for initialization
    void Start() {
        textUI.gameObject.SetActive(false);
        _finished = false;
    }

    // Update is called once per frame
    void Update() {
        if(_activated && !_finished) {
            _timer += Time.deltaTime;
            if(_timer > _time) {
                textUI.text = tutorialText;
                textUI.gameObject.SetActive(true);
            }
        }
        if (Input.GetAxis(axisName) > 0 && textUI.gameObject.activeSelf && !_finished) {
            textUI.gameObject.SetActive(false);
            _finished = true;
        }
    }

    public override void Activate() {
        _activated = true;
    }

    public override void Deactivate() {
    }
}
