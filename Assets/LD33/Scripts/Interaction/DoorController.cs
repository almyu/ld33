using JamSuite.Audio;
using UnityEngine;

public class DoorController : MonoSingleton<DoorController> {

    public string sfx;

    private GameObject _door;
    private Light _hallLight;
    private LightSpotter _lightSpotter;
    private float _elapsed = 0.0f;
    private float _doorOpenAngle = -60.0f;
    private float _doorCloseAngle = 60.0f;

    private void Awake() {
        _door = GameObject.FindGameObjectWithTag("Door");
        _hallLight = GetComponent<Light>();
        _lightSpotter = GetComponent<LightSpotter>();
    }

    private void Update() {
        _elapsed += Time.deltaTime;

        //WaitAndOpenDoor
        if (_elapsed < Balance.instance.WaitBeforeOpenDoor) {
            return;
        }
        else {
            if (!_lightSpotter.enabled) {
                _lightSpotter.enabled = true;
                _hallLight.enabled = true;
            }
        }

        if (_elapsed >= Balance.instance.DoorOpenedDuration + Balance.instance.WaitBeforeOpenDoor) {
            RotateDoor(_doorCloseAngle);
        }
        else {
            RotateDoor(_doorOpenAngle);
        }
        
        if (_elapsed >= Balance.instance.DoorOpenedDuration*1.5 + Balance.instance.WaitBeforeOpenDoor) {
            enabled = false;
        }
    }

    private void RotateDoor(float doorOpenAngle) {
        var str = Mathf.Min(0.5f * Time.deltaTime, 1);
        _door.transform.rotation = Quaternion.Lerp(_door.transform.rotation, Quaternion.AngleAxis(doorOpenAngle, Vector3.up), str);
    }
    
    private void OnEnable() {
        _elapsed = 0.0f;
        Sfx.Play(sfx);

    }

    private void OnDisable() {
        _lightSpotter.enabled = false;
        _hallLight.enabled = false;
    }
}
