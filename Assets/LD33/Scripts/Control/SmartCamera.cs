using UnityEngine;

public class SmartCamera : MonoBehaviour {

    public Vector3 offset;
    public float maxDistance = 1f;
    public float rotationDelay = 0.5f;

    private Transform player;
    private Vector3 lastTarget;
    private float inactivityTimer;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        var target = player.position + offset;

        if (target != lastTarget)
            inactivityTimer = rotationDelay;
        else {
            inactivityTimer -= Time.deltaTime;

            if (inactivityTimer <= 0f) {
                transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, -inactivityTimer);
                //transform.position = Vector3.Lerp(transform.rotation, )
            }
        }

        lastTarget = target;

        transform.position = target + (transform.position - target).normalized * maxDistance;
    }
}
