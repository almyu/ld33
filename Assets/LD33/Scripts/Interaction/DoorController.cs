using JamSuite.Audio;
using UnityEngine;

public class DoorController : MonoSingleton<DoorController> {

    public string sfx;

    private GameObject _door;
    private Light _hallLight;
    private LightSpotter _lightSpotter;
    private float _elapsed = 0.0f;
    private Animator _animation;

    private void Awake() {
        _door = GameObject.FindGameObjectWithTag("Door");
        _hallLight = GetComponent<Light>();
        _lightSpotter = GetComponent<LightSpotter>();
        _animation = _door.GetComponent<Animator>();
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
            _animation.Play("DoorClose");
        }
        else {
            _animation.Play("DoorOpen");
        }
        
        if (_elapsed >= Balance.instance.DoorOpenedDuration*1.5 + Balance.instance.WaitBeforeOpenDoor) {
            enabled = false;
        }
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
