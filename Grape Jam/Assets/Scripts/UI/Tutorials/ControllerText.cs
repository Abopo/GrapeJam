using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerText : MonoBehaviour {
    Transform controllerText;

	// Use this for initialization
	void Start () {
        controllerText = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update () {
        if (LevelManager.UsingController()) {
            controllerText.gameObject.SetActive(false);
        } else {
            controllerText.gameObject.SetActive(true);
        }
    }
}
