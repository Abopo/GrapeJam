using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour {

    [SerializeField] RectTransform ScrollView = null;
    [SerializeField] Transform ContentPanel = null;
    [SerializeField] Button ResolutionButtonPrefab = null;

    Resolution[] _resolutions;
    Resolution _selectedResolution;
    List<Button> ResolutionButtons;

	// Use this for initialization
	void Start () {
        _resolutions = Screen.resolutions;
        ResolutionButtons = new List<Button>();

        AddButtons();

        //if(ResolutionsDropdown != null)
        //{
        //    foreach(var resolution in _resolutions)
        //    {
        //        ResolutionsDropdown.AddOptions(resolution.ToString());
        //    }
        //    ResolutionsDropdown.AddOptions(_resolutions);
        //}
	}

    void AddButtons()
    {
        if (ContentPanel == null)
            return;

        for (UInt32 i = 0; i < _resolutions.Length; i++)
        {
            Button newButton = Instantiate(ResolutionButtonPrefab, ContentPanel.position + new Vector3(ScrollView.rect.width * 0.5f, -(i * 20), 0), Quaternion.identity);
            newButton.transform.SetParent(ContentPanel);
            newButton.onClick.AddListener(OnClick);
            newButton.GetComponentInChildren<Text>().text =  String.Format("{0}x{1}", _resolutions[i].width, _resolutions[i].height);
            ResolutionButtons.Add(newButton);
        }
    }

    void OnClick()
    {
        Debug.Log(_selectedResolution);
        _selectedResolution = _resolutions[ResolutionButtons.IndexOf(gameObject.GetComponent<Button>())];
    }

    void SetResolution()
    {
        Screen.SetResolution(_selectedResolution.width, _selectedResolution.height, Screen.fullScreen);
    }

    void SelectResolution()
    {
        _selectedResolution = _resolutions[ResolutionButtons.IndexOf(ResolutionButtonPrefab)];
    }
}
