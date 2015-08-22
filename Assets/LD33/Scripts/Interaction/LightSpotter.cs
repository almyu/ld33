﻿using UnityEngine;
using UnityEngine.Events;

public class LightSpotter : MonoBehaviour {
    public UnityEvent OnPlayerSpotted;

    private GameObject _player;
    private float _fieldOfViewAngle;
    private float _alertToAdd = 0.0f;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _fieldOfViewAngle = GetComponent<Light>().spotAngle;
    }

    private void Update() {
        var direction = _player.transform.position - transform.position;
        var angle = Vector3.Angle(direction, transform.forward);
        if (angle < _fieldOfViewAngle * 0.5f) {
            RaycastHit hit;
            //Debug.DrawRay(transform.position, direction.normalized, Color.red, 10.0f);
            if (Physics.Raycast(transform.position, direction.normalized, out hit, Mathf.Infinity)) {
                if (hit.collider.gameObject.tag == "Player") {

                    _alertToAdd += Time.deltaTime* Balance.instance.SpottedPlayerAlarmFactor;
                    AlertCounter.instance.Add(_alertToAdd);

                    if (OnPlayerSpotted != null) {
                        OnPlayerSpotted.Invoke();
                    }
                }
            }
        }
    }

    private void OnEnable() {
        _alertToAdd = 0.0f;
    }
}
