﻿using JamSuite.Audio;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoSingleton<FlashlightController> {

    public string sfx;
    public float patrolToNewTarget;

    private GameObject[] _pointsForPatrol;
    private Light _flashlight;
    private float strength = 0.5f;
    private float _elapsed = 0.0f;
    private const float SmoothTime = 0.2F;
    private GameObject _player;
    private LightSpotter _lightSpotter;
    private bool _followPlayer = false;
    private bool _generateNewTarget = true;
    private Vector3 _target;

    private void Awake() {
        _flashlight = GetComponent<Light>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _lightSpotter = GetComponent<LightSpotter>();
        _pointsForPatrol = GameObject.FindGameObjectsWithTag("PointsForSauron");
        _target = GenerateNewTarget();
    }

    private void Update() {
        _elapsed += Time.deltaTime;
        if(_elapsed >= patrolToNewTarget) {
            _generateNewTarget = true;
            _elapsed = 0.0f;
        }

        if (!_followPlayer) {
            Patrol();
        }
        else {
            StartFollowPlayer();
        }
    }
    
    private void StartFollowPlayer() {
        var targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        var str = Mathf.Min(strength * Time.deltaTime, 1);
        _flashlight.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    private void Patrol() {
        if (_generateNewTarget) {
            _target = GenerateNewTarget();
        }

        var targetRotation = Quaternion.LookRotation(_target - transform.position);
        var str = Mathf.Min(strength * Time.deltaTime, 1);
        _flashlight.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    private Vector3 GenerateNewTarget() {
        var r = Random.Range(0, _pointsForPatrol.Length);
        _generateNewTarget = false;
        return _pointsForPatrol[r].transform.position;
    }
    
    private void OnDisable() {
        _lightSpotter.enabled = false;
        _flashlight.enabled = false;
    }

    public void FollowPlayer(bool follow) {
        _followPlayer = follow;
    }
}
