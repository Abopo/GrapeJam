using System;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour {

    [SerializeField] Toggle toggle = null;

	// Use this for initialization
	void Start () {
        toggle.isOn = Screen.fullScreen;
	}

    public void ToggleFullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen));
    }
}