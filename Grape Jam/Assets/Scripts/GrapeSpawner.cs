using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour {

    [SerializeField] GrapeSwarm Swarm = null;
    [SerializeField] Grape Grape = null;
    [SerializeField] Transform Parent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (Grape == null || Parent == null)
            return;

        if(other.tag == "Grape")
        {
            // TODO: Check minimum number of grapes for level

            //Transform position = this.transform;
            //position.Translate(new Vector3(0, 1, 0));
            Swarm.AddGrape(Instantiate(Grape, transform.position, Quaternion.identity));
        }
    }
}
