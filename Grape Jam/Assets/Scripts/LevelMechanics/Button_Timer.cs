using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Timer : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] ActivateableObject ObjectToActivate = null;
    [SerializeField] float Duration = 10.0f;

    float _elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Turn deactivate object after set time has elapsed
        if(_elapsedTime > Duration) {
            _elapsedTime = 0.0f;
            Active = false;
            ObjectToActivate.Deactivate();
        }

        // Keeps reccord of time since activation
        if (Active) {
            _elapsedTime += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (ObjectToActivate == null)
            return;

        if(other.tag == "Grape" && !Active)
        {
            Active = true;
            ObjectToActivate.Activate();
        }
    }
}