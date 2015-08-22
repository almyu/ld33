using UnityEngine;

public class FlashlightController : MonoSingleton<FlashlightController> {
    private Light _flashlight;
    private float elapsed = 0.0f;
    private float strength = 0.5f;
    
    private const float SmoothTime = 0.2F;
    private GameObject _player;
    private LightSpotter _lightSpotter;

    private void Awake() {
        _flashlight = GetComponent<Light>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _lightSpotter = GetComponent<LightSpotter>();
    }

    private void Update() {
        if(elapsed >= Balance.instance.FlashightDuration) {
            enabled = false;
            return;
        }
        
        var targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        var str = Mathf.Min(strength * Time.deltaTime, 1);
        _flashlight.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

        elapsed += Time.deltaTime;
    }

    private void OnEnable() {
        _lightSpotter.enabled = true;
        _flashlight.enabled = true;
        elapsed = 0.0f;
    }

    private void OnDisable() {
        _lightSpotter.enabled = false;
        _flashlight.enabled = false;
    }
}
