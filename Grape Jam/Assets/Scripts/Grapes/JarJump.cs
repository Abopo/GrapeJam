using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarJump : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape") {
            other.GetComponent<Grape>().JumpIntoJar(this.transform);
        }
    }
}
