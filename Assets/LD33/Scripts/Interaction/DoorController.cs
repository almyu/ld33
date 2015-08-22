using UnityEngine;

public class DoorController : MonoSingleton<DoorController> {
    public GameObject Door;

    private Light _hallLight;
    private LightSpotter _lightSpotter;
    private float _elapsed = 0.0f;
    private float _doorOpenAngle = -60.0f;
    private float _doorCloseAngle = 60.0f;
    private Quaternion _doorClosedState;

    private void Awake() {
        _hallLight = GetComponent<Light>();
        _lightSpotter = GetComponent<LightSpotter>();
        _doorClosedState = Door.transform.rotation;
    }

    private void Update() {
        if (_elapsed >= Balance.instance.DoorOpenedDuration) {
            if (Door.transform.rotation != _doorClosedState) {
                RotateDoor(_doorCloseAngle);
            }
            else {
                enabled = false;
            }
            return;
        }

        RotateDoor(_doorOpenAngle);

        _elapsed += Time.deltaTime;
    }

    private void RotateDoor(float doorOpenAngle) {
        var str = Mathf.Min(0.5f * Time.deltaTime, 1);
        Door.transform.rotation = Quaternion.Lerp(Door.transform.rotation, Quaternion.AngleAxis(doorOpenAngle, Vector3.up), str);
    }
    
    private void OnEnable() {
        if (_elapsed > float.Epsilon)
            return;
        _lightSpotter.enabled = true;
        _hallLight.enabled = true;
    }

    private void OnDisable() {
        _lightSpotter.enabled = false;
        _hallLight.enabled = false;
        _elapsed = 0.0f;
        Door.transform.rotation = _doorClosedState;
    }
}
