using UnityEngine;
using JamSuite.Audio;

public class NoisyObject : MonoBehaviour {

    public string sfx;
    public float alert = 1f;
    public float minCollisionSpeed = 1f;
    public LayerMask collisionLayers = 1; // default

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.sqrMagnitude < minCollisionSpeed * minCollisionSpeed)
            return;

        foreach (var contact in collision.contacts) {
            if ((contact.otherCollider.gameObject.layer & collisionLayers.value) == 0) continue;

            Sfx.Play(sfx);
            //AlertCounter.instance.Add(alert);
        }
    }
}
