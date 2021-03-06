﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonFunctions : MonoBehaviour {

    [SerializeField] Button TheButton;
    PauseManager _pauseManager = null;

    public void Start()
    {
        if (_pauseManager == null)
            _pauseManager = FindObjectOfType<PauseManager>();


        //TheButton.OnSubmit(TheButton.onClick);
        
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

        _pauseManager.SetState(PauseManager.PauseState.NotPaused);

        // TODO: Send player back to the main menu
        GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>().Wipe("Main Menu");
    }

    public void OnClick_Back()
    {
        if (_pauseManager == null)
            return;

        _pauseManager.SetState(PauseManager.PauseState.Paused_Default);
    }
}