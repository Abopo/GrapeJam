using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIcon : MonoBehaviour {
    public string levelName;
    public string sceneName;
    public int index;

    LevelSelectMenu _levelSelect;

	// Use this for initialization
	void Start () {
        _levelSelect = GameObject.FindObjectOfType<LevelSelectMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter() {
        // set level stuff
        _levelSelect.SetSelection(index);
    }

    private void OnMouseDown() {
        _levelSelect.LoadLevel();
    }
}
