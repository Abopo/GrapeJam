using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSpeedSlider : MonoBehaviour {

    [SerializeField] Slider SpeedSlider = null;
    [SerializeField] Text CameraSpeedSliderValueText = null;

    GrapeCamera _grapeCamera = null;

	// Use this for initialization
	void Start () {
        if (_grapeCamera == null)
            _grapeCamera = FindObjectOfType<GrapeCamera>();

        if (SpeedSlider == null)
            return;

        SpeedSlider.value = (PlayerPrefs.GetFloat("CameraSpeed", 75) - 50) / 100;
        CameraSpeedSliderValueText.text = ((int)(100.0f * SpeedSlider.value + 50)).ToString();
    }

    public void OnValueChanged()
    { 
        if(SpeedSlider == null)
            return;

        PlayerPrefs.SetFloat("CameraSpeed", SpeedSlider.value * 100 + 50);

        if (CameraSpeedSliderValueText != null)
            CameraSpeedSliderValueText.text = ((int)(100.0f * SpeedSlider.value + 50)).ToString();

        if(_grapeCamera != null)
            _grapeCamera.SetCameraSpeed(SpeedSlider.value * 100 + 50);
    }
}