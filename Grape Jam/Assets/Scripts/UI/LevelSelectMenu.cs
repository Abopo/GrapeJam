using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
    public Transform selectionBorder;
    public LevelIcon[] levels;
    public Text levelNameText;
    ScreenWipe _screenWipe;
    AudioSource _audioSource;

    int _curSelection;
    bool _justMoved;

    // Use this for initialization
    void Start() {
        _screenWipe = GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>();
        _audioSource = GetComponent<AudioSource>();

        _curSelection = 0;
        _justMoved = false;

        UpdateSelection();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Horizontal") > 0.2f && !_justMoved) {
            // Move Right
            MoveRight();
            _justMoved = true;
        } else if (Input.GetAxis("Horizontal") < -0.2f && !_justMoved) {
            // Move Left
            MoveLeft();
            _justMoved = true;
        } else if(Input.GetAxis("Horizontal") < 0.2f && Input.GetAxis("Horizontal") > -0.2) {
            _justMoved = false;
        }

        if(Input.GetButton("Jump")) {
            LoadLevel();
        }
        if(Input.GetButton("Cancel")) {
            MainMenu();
        }
    }

    void MoveRight() {
        _curSelection++;
        if(_curSelection >= levels.Length) {
            _curSelection = 0;
        }
        _audioSource.Play();
        UpdateSelection();
    }

    void MoveLeft() {
        _curSelection--;
        if(_curSelection < 0) {
            _curSelection = levels.Length - 1;
        }
        _audioSource.Play();
        UpdateSelection();
    }

    void UpdateSelection() {
        levelNameText.text = "\"" + levels[_curSelection].levelName + "\"";
        selectionBorder.position = levels[_curSelection].transform.position;
    }

    public void MainMenu() {
        _screenWipe.Wipe("Main Menu");
    }

    public void SetSelection(int index) {
        _curSelection = index;
        _audioSource.Play();
        UpdateSelection();
    }

    public void LoadLevel() {
        _screenWipe.Wipe(levels[_curSelection].sceneName);
    }
}
