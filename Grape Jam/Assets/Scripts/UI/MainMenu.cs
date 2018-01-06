using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Transform selectionBorder;
    public Button[] buttons;
    public Credits credits;

    ScreenWipe _screenWipe;
    AudioSource _audioSource;

    int _curSelection;
    bool _justMoved;

    // Use this for initialization
    void Start () {
        _screenWipe = GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>();
        _audioSource = GetComponent<AudioSource>();

        _curSelection = 0;
        _justMoved = false;

        // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update () {
        if (credits.isDisplayed) {
            return;
        }

        if (Input.GetAxis("Vertical") > 0.2f && !_justMoved) {
            MoveDown();
            _justMoved = true;
        } else if (Input.GetAxis("Vertical") < -0.2f && !_justMoved) {
            MoveUp();
            _justMoved = true;
        } else if (Input.GetAxis("Vertical") < 0.2f && Input.GetAxis("Vertical") > -0.2) {
            _justMoved = false;
        }

        if (Input.GetButtonDown("Jump")) {
            /*
            if (buttons[_curSelection].sceneName == "Exit") {
                Application.Quit();
            } else {
                _screenWipe.Wipe(buttons[_curSelection].sceneName);
            }
            */
            buttons[_curSelection].onClick.Invoke();
        }
    }

    void MoveUp() {
        _curSelection++;
        if (_curSelection >= buttons.Length) {
            _curSelection = 0;
        }
        _audioSource.Play();
        selectionBorder.position = buttons[_curSelection].transform.position;
    }

    void MoveDown() {
        _curSelection--;
        if (_curSelection < 0) {
            _curSelection = buttons.Length - 1;
        }
        _audioSource.Play();
        selectionBorder.position = buttons[_curSelection].transform.position;
    }

    public void SetSelection(int index) {
        _curSelection = index;
        _audioSource.Play();
        selectionBorder.position = buttons[_curSelection].transform.position;
    }
}
