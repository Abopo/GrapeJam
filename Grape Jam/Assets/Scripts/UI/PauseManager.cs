using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField] GameObject DefaultSelection = null;
    [SerializeField] GameObject DefaultSelectionSettings = null;


    PauseState _currentState = PauseState.NotPaused;
    float _gameTimescale = 1;

	// Use this for initialization
	void Start () {
        PauseMenu.SetActive(false);
        _gameTimescale = Time.timeScale;
	}

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case PauseState.NotPaused:
                { 
                    if(Input.GetButtonDown("Pause"))
                    {
                        SetState(PauseState.Paused_Default);
                    }
                }
                break;
            case PauseState.Paused_Default:
                {
                    if(Input.GetButtonDown("Pause"))
                    {
                        SetState(PauseState.NotPaused);
                    }

                    if (Input.GetButtonDown("Submit"))
                    {
                        var currentButton = EventSystem.current.GetComponent<Button>();

                        if (currentButton != null)
                            currentButton.onClick.Invoke();
                    }

                    if(Input.GetButtonDown("Cancel"))
                    {
                        SetState(PauseState.NotPaused);
                    }

                }
                break;
            case PauseState.Paused_Settings:
                {
                    if (Input.GetButtonDown("Pause"))
                    {
                        SetState(PauseState.NotPaused);
                    }

                    if (Input.GetButtonDown("Submit"))
                    {
                        var currentButton = EventSystem.current.GetComponent<Button>();

                        if (currentButton != null)
                            currentButton.onClick.Invoke();
                    }

                    if(Input.GetButtonDown("Cancel"))
                    {
                        SetState(PauseState.Paused_Default);
                    }
                }
                break;
            default:
                break;
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

        // Hide cursor and keep it in the window
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void PauseGame() {
        _gameTimescale = Time.timeScale;
        Time.timeScale = 0;
        PauseShadow.SetActive(true);

        // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DisplaySettingsMenu() {
        PauseSettingsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(DefaultSelectionSettings);
    }

    void DisplayDefaultPauseMenu() {
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(DefaultSelection);
    }

    void HideDefaultPauseMenu() {
        EventSystem.current.SetSelectedGameObject(null);
        PauseMenu.SetActive(false);
    }

    void HidePauseSettingsMenu() {
        EventSystem.current.SetSelectedGameObject(null);
        PauseSettingsMenu.SetActive(false);
    }
}