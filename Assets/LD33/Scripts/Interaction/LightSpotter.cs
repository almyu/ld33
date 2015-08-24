using UnityEngine;
using UnityEngine.Events;

public class LightSpotter : MonoBehaviour {

    [Range(0, 1)]
    public float rangeFactor = 1f;

    public float alertPerSecond = 40f;

    public UnityEvent OnPlayerSpotted;
    public UnityEvent OnPlayerLost;

    [HideInInspector]
    public bool iSeePlayer;

    private Collider _player;
    private float _fieldOfViewAngle;
    private Light _light;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        _light = GetComponent<Light>();
        _fieldOfViewAngle = _light.spotAngle;
    }

    private void Update() {
        var direction = _player.bounds.center - transform.position;
        var angle = Vector3.Angle(direction, transform.forward);
        if (angle < _fieldOfViewAngle * 0.5f) {
            RaycastHit hit;
            //Debug.DrawRay(transform.position, direction.normalized, Color.red, 10.0f);
            if (Physics.Raycast(transform.position, direction.normalized, out hit, _light.range * rangeFactor)) {
                if (hit.collider == _player) {
                    if (!iSeePlayer) {
                        iSeePlayer = true;
                        OnPlayerSpotted.Invoke();
                    }

                    AlertCounter.instance.Add(alertPerSecond * Time.deltaTime);
                    return;
                }
            }
        }
        if (iSeePlayer) {
            iSeePlayer = false;
            OnPlayerLost.Invoke();
        }
    }

    private void OnEnable() {
        alertPerSecond = 0.0f;
    }
}
