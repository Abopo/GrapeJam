using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerNavigation : MonoBehaviour {

    [SerializeField] Selectable DefaultSelection = null;
    [SerializeField] List<Selectable> UIObjects = null;
    
    Selectable _currentlySelected;
    int _selectedIndex = 0;
    bool _stickReset = true;


	// Use this for initialization
	void Start () {
        DefaultSelection.Select();  
	}

    private void OnEnable()
    {
        SelectDefault();
    }

    private void OnDisable()
    {
        Deselect();
    }

    // Update is called once per frame
    void Update () {

        //var test = EventSystem.current.GetComponent<Button>();
        //
        //if(Input.GetButtonDown("Submit"))
        //    test.onClick.Invoke();



    }

    // Selects default control, if default is not set, selects furst control in list
    public void SelectDefault()
    {
        if (DefaultSelection != null)
            DefaultSelection.Select();
        else if (UIObjects.Count > 0)
            UIObjects[0].Select();
    }

    public void Deselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Old()
    {
        if (_stickReset)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (_selectedIndex == 0)
                    _selectedIndex = UIObjects.Count - 1;
                else
                    _selectedIndex--;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                if (_selectedIndex == UIObjects.Count - 1)
                    _selectedIndex = 0;
                else
                    _selectedIndex++;
            }

            _stickReset = false;
        }

        //UIObjects[_selectedIndex].Select();

        if (Mathf.Abs((Input.GetAxis("Vertical"))) < 0.05f)
            _stickReset = true;
    }
}
