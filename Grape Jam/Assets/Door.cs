using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivateableObject {

    [SerializeField] bool AllowToggle = false;

    public override bool Activate()
    {
        if (AllowToggle)
            gameObject.SetActive(!gameObject.activeSelf);

        else
            gameObject.SetActive(false);

        return true;
    }

    public override bool Deactivate()
    {
        return true;
    }
}