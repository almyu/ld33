using JamSuite.Audio;
using UnityEngine;

public class FlashlightController : MonoSingleton<FlashlightController> {

    public string sfx;

    private Light _flashlight;
    private float elapsed = 0.0f;
    private float strength = 0.5f;
    private bool _firstTime = true;
    private const float SmoothTime = 0.2F;
    private GameObject _player;
    private LightSpotter _lightSpotter;
    private Vector3 _oldPlayerPosition;

    private void Awake() {
        _flashlight = GetComponent<Light>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _lightSpotter = GetComponent<LightSpotter>();
    }

    private void Update() {
        elapsed += Time.deltaTime;

        //WaitAndTurnOnFlashLight
        if (elapsed < Balance.instance.WaitBeforeOpenDoor) {
            return;
        }
        else {
            if (!_lightSpotter.enabled) {
                _lightSpotter.enabled = true;
                _flashlight.enabled = true;
            }
        }

        if (elapsed >= Balance.instance.FlashightDuration + Balance.instance.WaitBeforeOpenDoor) {
            enabled = false;
            return;
        }

        if (!_firstTime) {
            FollowPlayer(_player.transform.position);
        }
        else {
            FollowPlayer(_oldPlayerPosition);
            _firstTime = false;
        }
    }
    
    private void FollowPlayer(Vector3 playerPosition) {
        var targetRotation = Quaternion.LookRotation(playerPosition - transform.position);
        var str = Mathf.Min(strength * Time.deltaTime, 1);
        _flashlight.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    private void OnEnable() {
        _oldPlayerPosition = _player.transform.position;
        _flashlight.transform.rotation = Quaternion.LookRotation(_oldPlayerPosition);
        Sfx.Play(sfx);
        elapsed = 0.0f;
    }

    private void OnDisable() {
        _lightSpotter.enabled = false;
        _flashlight.enabled = false;
    }
}
