using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSet : ActivateableObject {

    [SerializeField] ActivateableObject ObjectToActivate = null;

    [SerializeField] int requiredPlates = 1;
    int plateCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Activate() {
        plateCount += 1;
        if(plateCount >= requiredPlates) {
            ObjectToActivate.Activate();
        }
    }

    public override void Deactivate() {
        plateCount -= 1;
        if(plateCount < requiredPlates) {
            ObjectToActivate.Deactivate();
        }
    }

    public override void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
