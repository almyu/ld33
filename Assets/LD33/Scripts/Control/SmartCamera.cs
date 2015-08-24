using UnityEngine;

public class SmartCamera : MonoBehaviour {

    public LayerMask ignoredLayers;

    private float initialZ;
    private Vector2 nearDiag;

    private void Awake() {
        initialZ = transform.localPosition.z;

        var cam = GetComponent<Camera>();
        nearDiag = new Vector2(cam.aspect, 1f) * cam.nearClipPlane / Mathf.Cos(cam.fieldOfView * Mathf.Deg2Rad * 0.5f);
    }

    private void LateUpdate() {
        Ray ray = new Ray(transform.parent.position, -transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, ~ignoredLayers.value)) {
            var n = transform.InverseTransformDirection(hit.normal);
            var zNear = Mathf.Max(Mathf.Abs(n.y * nearDiag.y), Mathf.Abs(n.x * nearDiag.x)) * 2f;

            transform.localPosition = Vector3.back * Mathf.Clamp(hit.distance - zNear, 0f, -initialZ);
        }
        else transform.localPosition = Vector3.forward * initialZ;
    }
}
