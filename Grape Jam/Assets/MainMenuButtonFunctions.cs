using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonFunctions : MonoBehaviour {

    public void LevelSelect_OnClick()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Options_OnClick()
    {
        SceneManager.LoadScene("Options");
    }

    public void Exit_OnClick()
    {
        Application.Quit();
    }
}