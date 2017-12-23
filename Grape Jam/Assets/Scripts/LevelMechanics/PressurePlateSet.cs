using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSet : ActivateableObject {

    [SerializeField] ActivateableObject[] ObjectsToActivate;

    [SerializeField] int requiredPlates = 1;
    int plateCount = 0;
    bool _done = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Activate() {
        plateCount += 1;
        if(plateCount >= requiredPlates) {
            foreach (ActivateableObject ao in ObjectsToActivate) {
                ao.Activate();
            }
            _done = true;
        }

        return _done;
    }

    public override bool Deactivate() {
        plateCount -= 1;
        if(plateCount < 0) {
            plateCount = 0;
        }
        /*
        if(plateCount < requiredPlates) {
            foreach (ActivateableObject ao in ObjectsToActivate) {
                ao.Deactivate();
            }
        }
        */
        return _done;
    }

    public override void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
