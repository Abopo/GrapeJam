using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomTutorial : MonoBehaviour {

    public Text textUI;
    public string controllerTutorialText;
    public string keyboardTutorialText;

    bool _finished;
    bool _usingController;

    // Use this for initialization
    void Start() {
        textUI.gameObject.SetActive(false);
        _finished = false;

        if (LevelManager.UsingController()) {
            _usingController = true;
        } else {
            _usingController = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if ((Input.GetAxis("CameraZoom") > 0 || Input.GetKey(KeyCode.UpArrow)) && textUI.gameObject.activeSelf && !_finished) {
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
