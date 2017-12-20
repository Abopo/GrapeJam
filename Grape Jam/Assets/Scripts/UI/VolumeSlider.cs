using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    enum SliderType
    {
        Master,
        BGM,
        SFX
    }

    [SerializeField] Slider AudioSlider = null;
    [SerializeField] Text ValueText = null;
    [SerializeField] SliderType Type = SliderType.Master;

    AudioManager _audioManager = null;

	// Use this for initialization
	void Start () {

        if (_audioManager == null)
            _audioManager = FindObjectOfType<AudioManager>();

        switch(Type)
        {
            case SliderType.Master:
                AudioSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
                break;
            case SliderType.BGM:
                AudioSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
                break;
            case SliderType.SFX:
                AudioSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
                break;
            default:
                break;
        }
	}

    public void OnValueChange() {
        if (_audioManager == null || AudioSlider == null)
            return;

        switch(Type)
        {
            case SliderType.Master:
                PlayerPrefs.SetFloat("MasterVolume", AudioSlider.value);
                break;
            case SliderType.BGM:
                PlayerPrefs.SetFloat("BGMVolume", AudioSlider.value);
                break;
            case SliderType.SFX:
                PlayerPrefs.SetFloat("SFXVolume", AudioSlider.value);
                Debug.Log(PlayerPrefs.GetFloat("SFXVolume"));
                PlayerPrefs.Save();
                break;
            default:
                break;
        }

        if (ValueText != null)
            ValueText.text = ((int)(AudioSlider.value * 100)).ToString();

        _audioManager.SetVolume();
    }
}