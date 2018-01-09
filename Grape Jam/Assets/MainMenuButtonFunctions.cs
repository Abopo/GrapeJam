using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour {
    public int index;
    public Credits creditsObj;

    ScreenWipe _screenWipe;
    MainMenu _mainMenu;

    private void Start() {
        _screenWipe = GameObject.FindGameObjectWithTag("ScreenWipe").GetComponent<ScreenWipe>();
        _mainMenu = GameObject.FindObjectOfType<MainMenu>();
    }

    public void LevelSelect_OnClick()
    {
        _screenWipe.Wipe("LevelSelect");
        //SceneManager.LoadScene("LevelSelect");
    }

    public void Options_OnClick()
    {
        _screenWipe.Wipe("Options");
        //SceneManager.LoadScene("Options");
    }

    public void Credits_OnClick() {
        // Show the credits
        creditsObj.Display();
    }

    public void Exit_OnClick()
    {
        Application.Quit();
    }

    private void OnMouseEnter() {
        // set level stuff
        _mainMenu.SetSelection(index);
    }
}