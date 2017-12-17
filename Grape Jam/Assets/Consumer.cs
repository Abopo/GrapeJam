using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour {

    [SerializeField] GrapeSwarm Swarm = null;
    [SerializeField] ActivateableObject ObjectToActivate = null;
    [SerializeField] int RequiredGrapes = 1;
    [SerializeField] bool Active = false;

    List<GameObject> _consumedGrapes = null;

	// Use this for initialization
	void Start () {
        _consumedGrapes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ObjectToActivate == null)
            return;

		if(_consumedGrapes.Count >= RequiredGrapes && !Active) {
            Active = true;
            ObjectToActivate.Activate();
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (Swarm == null || ObjectToActivate == null)
            return;

        if(other.tag == "Grape" && _consumedGrapes.Count < RequiredGrapes) {
            Swarm.LoseGrape(other.gameObject.GetComponent<Grape>());
            //TODO: We might need to kill velocity when removing grapes
            _consumedGrapes.Add(other.gameObject);
        }
    }
}
