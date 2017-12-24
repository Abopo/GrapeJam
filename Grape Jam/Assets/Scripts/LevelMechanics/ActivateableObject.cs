using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual bool Activate()
    {
        gameObject.SetActive(true);
        return true;
    }

    public virtual bool Deactivate()
    {
        gameObject.SetActive(false);
        return false;
    }

    public virtual void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
