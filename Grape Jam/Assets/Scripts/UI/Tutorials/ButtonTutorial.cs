using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTutorial : MonoBehaviour {

    public Text textUI;
    public string controllerTutorialText;
    public string keyboardTutorialText;
    public string buttonName;

    bool _finished;
    bool _usingController;

    // Use this for initialization
    void Start() {
        textUI.gameObject.SetActive(false);
        _finished = false;

        if (Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0] != "") {
            _usingController = true;
        } else {
            _usingController = false;
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown(buttonName) && textUI.gameObject.activeSelf && !_finished) {
            textUI.gameObject.SetActive(false);
            _finished = true;
        }
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Grape" && !_finished) {
            if (_usingController) {
                textUI.text = controllerTutorialText;
            } else {
                textUI.text = keyboardTutorialText;
            }
            textUI.gameObject.SetActive(true);
        }
    }
}
