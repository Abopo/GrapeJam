using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tries to keep grapes from falling back into the grape spawners
public class GrapeSpawnerTop : MonoBehaviour {
    public Collider _collider;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerExit(Collider other) {
        if (other.tag == "Grape") {
            Physics.IgnoreCollision(_collider, other, false);
        }
    }
}
