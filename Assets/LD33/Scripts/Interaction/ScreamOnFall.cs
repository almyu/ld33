using JamSuite.Audio;
using UnityEngine;

public class ScreamOnFall : MonoBehaviour {

    public string sfx;
    public float alert = 1f;
    public float minCollisionSpeed = 1f;

    private void OnCollisionEnter(Collision collision) {
        if (Mathf.Abs(collision.relativeVelocity.y) < minCollisionSpeed) {
            return;
        }
        
        foreach (var contact in collision.contacts) {
            Debug.Log("Ouch!");
            Sfx.Play(sfx);
            AlertCounter.instance.Add(alert);
        }
    }
}
