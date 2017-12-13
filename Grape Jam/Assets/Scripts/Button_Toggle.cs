using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Toggle : MonoBehaviour {

    [SerializeField] bool Active = false;
    [SerializeField] GameObject ObjectToToggle = null;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {

        if (ObjectToToggle == null)
            return;

        //TODO: Swap Between On/Off visually

        ObjectToToggle.SetActive(!ObjectToToggle.activeSelf);
        Active = !Active;
    }
}