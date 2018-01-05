using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public string nextLevel;
    public int parCompletionTimeInSeconds;
    public GameObject levelEndMenu;
    public Text ratingText; // Shows how well the player did (based on score)
    public Text levelTime; // Shows the level completion time
    public Text grapesJammedText; // Shows how many grapes made it to the jar
    public Text grapesLostText; // Shows how many grapes died
    public Text scoreText; // Shows total score

    float _levelPlayTime;
    int _grapesJammedCount;
    int _grapesLostCount;
    int _finalScore;

    bool _levelEnded;

    // Buffer to make sure player doesn't skip through end screen.
    float _bufferTime = 1.0f;
    float _bufferTimer = 0.0f;

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
            _bufferTimer += Time.deltaTime;
            if(Input.GetButtonDown("Jump") && _bufferTimer >= _bufferTime) {
                GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>().Wipe(nextLevel);
                //SceneManager.LoadScene(nextLevel);
            }
        } else {
            _levelPlayTime += Time.deltaTime;
        }

        // Testing buttons
        if(Input.GetKeyDown(KeyCode.N)) {
            GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>().Wipe(nextLevel);
            //SceneManager.LoadScene(nextLevel);
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
            int minutes = (int)(_levelPlayTime / 60);
            int seconds = (int)(_levelPlayTime % 60);
            string secondsString;
            if(seconds < 10) {
                secondsString = "0" + seconds;
            } else {
                secondsString = seconds.ToString();
            }
            levelTime.text = minutes + ":" + secondsString;
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
        if (_grapesLostCount < _grapesJammedCount * 4) {
            _finalScore += _grapesJammedCount * 100;
            _finalScore -= _grapesLostCount * 25;
        }

        scoreText.text = _finalScore.ToString();

        // Show appropriate rating for score
        if(_finalScore >= 2500) {
            ratingText.text = "Jammin'!!!";
        } else if(_finalScore >= 1500) {
            ratingText.text = "Jam Packed!";
        } else {
            ratingText.text = "Just Jelly...";
        }

        ratingText.transform.GetChild(0).GetComponent<Text>().text = ratingText.text;
    }
}
