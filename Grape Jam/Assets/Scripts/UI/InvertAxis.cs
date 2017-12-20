using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertAxis : MonoBehaviour {

    enum Axis { X, Y }

    [SerializeField] Toggle TheToggle = null;
    [SerializeField] Axis AxisToInvert = Axis.X;

    GrapeCamera _grapeCamera = null;

	// Use this for initialization
	void Start () {

        if (_grapeCamera == null)
            _grapeCamera = FindObjectOfType<GrapeCamera>();

        if (TheToggle == null)
            return;

        switch(AxisToInvert)
        {
            case Axis.X:
                int xInvert = PlayerPrefs.GetInt("XInverted");

                if (xInvert == 1)
                    TheToggle.isOn = false;
                else if (xInvert == -1)
                    TheToggle.isOn = true;

                break;
            case Axis.Y:
                int yInvert = PlayerPrefs.GetInt("YInverted");

                if (yInvert == 1)
                    TheToggle.isOn = false;
                else if (yInvert == -1)
                    TheToggle.isOn = true; break;
            default:
                break;
        }
		
	}

    public void OnToggle()
    {
        if (_grapeCamera == null || TheToggle == null)
            return;

        switch(AxisToInvert)
        {
            case Axis.X:
                int xInvert;

                if (TheToggle.isOn)
                    xInvert = -1;
                else
                    xInvert = 1;

                PlayerPrefs.SetInt("XInverted", xInvert);
                _grapeCamera.SetXInversion(TheToggle.isOn);

                break;
            case Axis.Y:
                int yInvert;

                if (TheToggle.isOn)
                    yInvert = -1;
                else
                    yInvert = 1;

                PlayerPrefs.SetInt("YInverted", yInvert);
                _grapeCamera.SetYInversion(TheToggle.isOn);

                break;
            default:
                break;
        }
    }
}
