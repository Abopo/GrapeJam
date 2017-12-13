using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour {

    [SerializeField] GrapeSwarm Swarm = null;
    [SerializeField] Grape Grape = null;
    [SerializeField] float SpawnRate = 1.0f;
    [SerializeField] int MaxGrapes = 10;

    float _timer = 0.0f;

	// Use this for initialization
	void Start () {
        _timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other) {
        SpawnGrape(other);
    }

    private void OnTriggerStay(Collider other) {
        SpawnGrape(other);
    }

    private void SpawnGrape(Collider other) {
        if (other.tag != "Grape")
            return;

        if (_timer > SpawnRate && Swarm.GetGrapeCount() < MaxGrapes) {
            Grape newGrape = Instantiate(Grape, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            Swarm.AddGrape(newGrape);
            _timer = 0.0f;
        }
    }
}