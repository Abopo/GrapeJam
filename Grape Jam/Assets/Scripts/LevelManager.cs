using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public string nextLevel;
    public int parCompletionTimeInSeconds;
    public GameObject levelEndMenu;
    public Text levelTime;
    public Text grapesJammedText;
    public Text grapesLostText;
    public Text scoreText;

    float _levelPlayTime;
    int _grapesJammedCount;
    int _grapesLostCount;
    int _finalScore;

    bool _levelEnded;

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0f, -9.81f * 2f, 0f);

        _levelPlayTime = 0;
        _grapesJammedCount = 0;
        _grapesLostCount = 0;
        _finalScore = 0;

        _levelEnded = false;
    }

    // Update is called once per frame
    void Update () {
        if(_levelEnded) {
            if(Input.GetButtonDown("Jump")) {
                SceneManager.LoadScene(nextLevel);
            }
        } else {
            _levelPlayTime += Time.deltaTime;
        }

        // Testing buttons
        if(Input.GetKeyDown(KeyCode.N)) {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void IncrementGrapesLost() {
        _grapesLostCount++;
        grapesLostText.text = _grapesLostCount.ToString();
    }

    public void IncrementGrapesJammed() {
        _grapesJammedCount++;
        grapesJammedText.text = _grapesJammedCount.ToString();
    }

    public void LevelEnd() {
        if (!_levelEnded) {
            levelTime.text = (int)(_levelPlayTime / 60) + ":" + (int)(_levelPlayTime % 60);
            grapesLostText.text = _grapesLostCount.ToString();
            grapesJammedText.text = _grapesJammedCount.ToString();

            CalculateScore();

            levelEndMenu.SetActive(true);
            _levelEnded = true;
        }
    }

    void CalculateScore() {
        int time = (int)(parCompletionTimeInSeconds - _levelPlayTime);
        _finalScore += time > 0 ? time * 10 : 0;
        _finalScore += _grapesJammedCount * 100;
        _finalScore -= _grapesLostCount * 50;

        scoreText.text = _finalScore.ToString();
    }
}
