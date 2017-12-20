using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtonFunctions : MonoBehaviour {

    PauseManager _pauseManager = null;

    public void Start()
    {
        if (_pauseManager == null)
            _pauseManager = FindObjectOfType<PauseManager>();
    }

    // Use this for initialization
    public void OnClick_Resume()
    {
        if (_pauseManager == null)
            return;

        _pauseManager.SetState(PauseManager.PauseState.NotPaused);
    }

    public void OnClick_Options()
    {
        if (_pauseManager == null)
            return;

        _pauseManager.SetState(PauseManager.PauseState.Paused_Settings);
    }

    public void OnClick_MainMenu()
    {
        if (_pauseManager == null)
            return;

        // TODO: Send player back to the main menu
    }

    public void OnClick_Back()
    {
        if (_pauseManager == null)
            return;

        _pauseManager.SetState(PauseManager.PauseState.Paused_Default);
    }
}