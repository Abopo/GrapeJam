using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {

    //TODO: Get Levels correctly
    [SerializeField] List<string> Levels = null;
    [SerializeField] Button LevelButtonPrefab = null;
    [SerializeField] RectTransform LevelPanelRectTransform = null;

    RectTransform _levelButtonRectTransform;
    float _heightOffest;
    float _widthOffset;
    
	// Use this for initialization
	void Start () {

        _levelButtonRectTransform = LevelButtonPrefab.GetComponent<RectTransform>();
        _heightOffest = _levelButtonRectTransform.rect.height * 0.5f;
        _widthOffset = _levelButtonRectTransform.rect.width * 0.5f;

        GenerateLevelButtons();
    }
	
    void GenerateLevelButtons()
    {
        float xSpacing = 100;
        float ySpacing = 100;
        int currentRow = 0;
        int currentColumn = 0;

        for (int i = 0; i < Levels.Count; i++)
        {
            if (i % 5 == 0 && i > 0)
            {
                currentRow++;
                currentColumn = 0;
            }

            float xPos = xSpacing * currentColumn + _widthOffset;
            float yPos = ySpacing * -currentRow - _heightOffest;

            Button newButton = Instantiate(LevelButtonPrefab, new Vector3(xPos, yPos, 0) + LevelPanelRectTransform.position, Quaternion.identity);
            newButton.transform.SetParent(LevelPanelRectTransform);
            newButton.GetComponentInChildren<Text>().text = string.Format("Level {0}", i + 1);

            currentColumn++;
        }
    }
}