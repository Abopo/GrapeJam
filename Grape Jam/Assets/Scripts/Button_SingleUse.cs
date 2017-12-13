using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SingleUse : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] GameObject ObjectToToggle = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (ObjectToToggle == null)
            return;

        //TODO: Swap Between On/Off visually

        if (!Active)
        {
            Active = true;
            ObjectToToggle.SetActive(!ObjectToToggle.activeSelf);
        }
    }
}
