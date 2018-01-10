using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivateableObject {

    public override bool Activate()
    {
        gameObject.SetActive(false);
        return true;
    }
}
