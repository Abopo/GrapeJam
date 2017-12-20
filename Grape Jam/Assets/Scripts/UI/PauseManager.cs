using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public enum PauseState
    {
        NotPaused,
        Paused_Default,
        Paused_Settings
    }

    [SerializeField] GameObject PauseShadow = null;
    [SerializeField] GameObject PauseMenu = null;
    [SerializeField] GameObject PauseSettingsMenu = null;

    PauseState _currentState = PauseState.NotPaused;
    float _gameTimescale = 1;

	// Use this for initialization
	void Start () {
        PauseMenu.SetActive(false);
        _gameTimescale = Time.timeScale;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch(_currentState)
            {
                case PauseState.NotPaused:
                    SetState(PauseState.Paused_Default);
                    break;
                case PauseState.Paused_Default:
                    SetState(PauseState.NotPaused);
                    break;
                case PauseState.Paused_Settings:
                    SetState(PauseState.NotPaused);
                    break;
                default:
                    break;
            }
        }
    }

    public void SetState(PauseState newState) {
        switch(_currentState)
        {
            case PauseState.NotPaused:
                {
                    switch (newState)
                    {
                        // Pause the game initially and display the default pause menu
                        case PauseState.Paused_Default:
                            PauseGame();
                            DisplayDefaultPauseMenu();
                            _currentState = PauseState.Paused_Default;
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PauseState.Paused_Default:
                {
                    switch (newState)
                    {
                        // Unpause the game
                        case PauseState.NotPaused:
                            HideDefaultPauseMenu();
                            UnpauseGame();
                            _currentState = PauseState.NotPaused;
                            break;
                        // Change display to pause settings
                        case PauseState.Paused_Settings:
                            HideDefaultPauseMenu();                         
                            DisplaySettingsMenu();
                            _currentState = PauseState.Paused_Settings;
                            break;
                        default:
                            break;
                    }
                }
                break;
            case PauseState.Paused_Settings:
                switch (newState)
                {
                    // Unpause the game
                    case PauseState.NotPaused:
                        HidePauseSettingsMenu();
                        UnpauseGame();
                        _currentState = PauseState.NotPaused;
                        break;
                    // Change display to pause settings
                    case PauseState.Paused_Default:
                        HidePauseSettingsMenu();
                        DisplayDefaultPauseMenu();
                        _currentState = PauseState.Paused_Default;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    void UnpauseGame() {
        Time.timeScale = _gameTimescale;
        PauseShadow.SetActive(false);
    }

    void PauseGame() {
        _gameTimescale = Time.timeScale;
        Time.timeScale = 0;
        PauseShadow.SetActive(true);
    }

    void DisplaySettingsMenu() {
        PauseSettingsMenu.SetActive(true);
    }

    void DisplayDefaultPauseMenu() {
        PauseMenu.SetActive(true);
    }

    void HideDefaultPauseMenu() {
        PauseMenu.SetActive(false);
    }

    void HidePauseSettingsMenu() {
        PauseSettingsMenu.SetActive(false);
    }
}