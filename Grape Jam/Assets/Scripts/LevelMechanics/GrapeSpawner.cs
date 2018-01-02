﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeSpawner : MonoBehaviour {

    [SerializeField] GrapeSwarm Swarm = null;
    [SerializeField] Grape Grape = null;
    [SerializeField] float SpawnRate = 1f;
    [SerializeField] int MaxGrapes = 10;
    [SerializeField] Collider topCollider;

    AudioManager _audioManager = null;
    AudioSource _audioSource;

    float _timer = 0.0f;

    private void Awake() {
        _timer = 0.0f;
        Swarm = GameObject.FindGameObjectWithTag("GrapeSwarm").GetComponent<GrapeSwarm>();
        _audioManager = FindObjectOfType<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioManager.AddAudioSource(_audioSource);
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        _timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Grape") {
            ShootGrape(other);

            if (Swarm.respawnPoint != this) {
                Swarm.respawnPoint = this;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Grape") {
            ShootGrape(other);
        }
    }

    private void SpawnGrape(Collider other) {
        if (_timer > SpawnRate && Swarm.GetGrapeCount() < MaxGrapes) {
            Grape newGrape = Instantiate(Grape, transform.position + new Vector3(0, 4, 0), Quaternion.identity);
            Swarm.AddGrape(newGrape);

            if (_audioManager != null)
                _audioManager.AddAudioSource(newGrape.GetComponents<AudioSource>());

            _timer = 0.0f;
        }
    }


    private void SpawnGrape() {
        Grape newGrape = Instantiate(Grape, transform.position + new Vector3(0, 4, 0), Quaternion.identity);
        Swarm.AddGrape(newGrape);

        if (_audioManager != null)
            _audioManager.AddAudioSource(newGrape.GetComponents<AudioSource>());

        _timer = 0.0f;
    }

    private void ShootGrape(Collider other) {
        if (_timer > SpawnRate && Swarm.GetGrapeCount() < MaxGrapes) {
            Grape newGrape = Instantiate(Grape, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            newGrape.Shoot();
            Physics.IgnoreCollision(newGrape.GetComponent<SphereCollider>(), topCollider, true);

            Swarm.AddGrape(newGrape);

            if (_audioManager != null)
                _audioManager.AddAudioSource(newGrape.GetComponents<AudioSource>());

            _timer = 0.0f;
        }
    }

    private void ShootGrape() {
        Grape newGrape = Instantiate(Grape, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        newGrape.Shoot();
        Physics.IgnoreCollision(newGrape.GetComponent<SphereCollider>(), topCollider, true);

        Swarm.AddGrape(newGrape);

        if (_audioManager != null)
            _audioManager.AddAudioSource(newGrape.GetComponents<AudioSource>());

        _timer = 0.0f;
    }


    public void RespawnSwarm() {
        Swarm.transform.position = transform.position + new Vector3(0, 1, 0);
        ShootGrape();
    }
}