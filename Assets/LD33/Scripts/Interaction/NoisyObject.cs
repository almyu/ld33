using UnityEngine;
using JamSuite.Audio;

public class NoisyObject : MonoBehaviour {

    public string sfx;
    public int score = 99;
    public float alert = 1f;
    public float minCollisionSpeed = 1f;
    public LayerMask collisionLayers = 1; // default
    public float dragScore = 0f;
    public float dragAlert = 0f;

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.sqrMagnitude < minCollisionSpeed * minCollisionSpeed)
            return;

        foreach (var contact in collision.contacts) {
            if (((1 << contact.otherCollider.gameObject.layer) & collisionLayers.value) == 0) continue;

            Sfx.Play(sfx);
            AlertCounter.instance.Add(alert);
            ScoreCounter.instance.Add(score);
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (Mathf.Approximately(dragAlert, 0f) && Mathf.Approximately(dragScore, 0f)) return;
        if (collision.relativeVelocity.sqrMagnitude < minCollisionSpeed / 10) return;

        foreach (var contact in collision.contacts) {
            if (((1 << contact.otherCollider.gameObject.layer) & collisionLayers.value) == 0) continue;

            Sfx.Play(sfx, 0.4f);
            AlertCounter.instance.Add(dragAlert * Time.fixedDeltaTime);
            ScoreCounter.instance.Add(Mathf.FloorToInt(dragScore * Time.fixedDeltaTime));
        }
    }
}
