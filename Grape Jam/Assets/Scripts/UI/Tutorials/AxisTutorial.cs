using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisTutorial : MonoBehaviour {

    public Text textUI;
    public string tutorialText;
    public string axisName;

    bool _finished;

    // Use this for initialization
    void Start() {
        textUI.gameObject.SetActive(false);
        _finished = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis(axisName) > 0 && textUI.gameObject.activeSelf && !_finished) {
            textUI.gameObject.SetActive(false);
            _finished = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape" && !_finished) {
            textUI.text = tutorialText;
            textUI.gameObject.SetActive(true);
        }
    }
}