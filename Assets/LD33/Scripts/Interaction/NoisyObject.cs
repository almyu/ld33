using UnityEngine;
using JamSuite.Audio;

public class NoisyObject : MonoBehaviour {

    public string sfxOnBump; //, sfxOnFall;
    public float minBumpVelocity = 1f; //, minFallVelocity = 10f;

    private void OnCollisionEnter(Collision collision) {
        // Collide against floor/player layers
        if (collision.relativeVelocity.sqrMagnitude >= minBumpVelocity * minBumpVelocity)
            Sfx.Play(sfxOnBump);
    }
}
