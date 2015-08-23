using UnityEngine;

public class SmartCamera : MonoBehaviour {

    public LayerMask ignoredLayers;

    private float initialZ;

    private void Awake() {
        initialZ = transform.localPosition.z;
    }

    private void LateUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, -transform.forward, out hit, -initialZ, ~ignoredLayers.value))
            transform.localPosition = Vector3.back * hit.distance;
        else
            transform.localPosition = Vector3.forward * initialZ;
    }
}
