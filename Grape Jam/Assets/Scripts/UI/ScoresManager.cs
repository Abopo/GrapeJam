using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresManager : MonoBehaviour {
    public Text[] scores;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("FirstTimePlaying", 0);

        if (PlayerPrefs.GetInt("FirstTimePlaying") == 0) {
            PlayerPrefs.SetInt("Level0Score", 0);
            PlayerPrefs.SetInt("Level1Score", 0);
            PlayerPrefs.SetInt("Level2Score", 0);
            PlayerPrefs.SetInt("Level3Score", 0);
            PlayerPrefs.SetInt("Level4Score", 0);
            PlayerPrefs.SetInt("Level5Score", 0);
            PlayerPrefs.SetInt("Level6Score", 0);

            PlayerPrefs.SetInt("FirstTimePlaying", 1);
        }

        for (int i = 0; i < scores.Length; ++i) {
            scores[i].text = PlayerPrefs.GetInt("Level" + i + "Score").ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
