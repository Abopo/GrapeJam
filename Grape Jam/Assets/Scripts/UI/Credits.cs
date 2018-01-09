using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    public bool isDisplayed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isDisplayed && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(0))) {
            Hide();
        }
	}

    public void Display() {
        gameObject.SetActive(true);
        isDisplayed = true;
    }

    void Hide() {
        gameObject.SetActive(false);
        isDisplayed = false;
    }
}
