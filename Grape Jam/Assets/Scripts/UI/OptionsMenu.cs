using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")) {
            GameObject.FindObjectOfType<ScreenWipe>().Wipe("Main Menu");
        }
	}
}
