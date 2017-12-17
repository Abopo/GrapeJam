using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SingleUse : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] ActivateableObject ObjectToActivate = null;

    private void OnTriggerEnter(Collider other) {
        if (ObjectToActivate == null)
            return;

        //TODO: Swap Between On/Off visually

        if (other.tag == "Grape" && !Active) {
            Active = true;
            ObjectToActivate.Activate();
            //ObjectToActivate.Toggle();
        }
    }
}
