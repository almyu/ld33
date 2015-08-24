using UnityEngine;
using UnityEngine.Events;
using JamSuite.UI;
using JamSuite.Audio;

[RequireComponent(typeof(SphereCollider))]
public class Trigger : MonoBehaviour {

    public string text = "Boo!";
    public string[] sfxVariants;
    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other) {
        FloatingText.instance.Spawn(transform.position, 0).text = text;
        onTrigger.Invoke();
        ScoreCounter.instance.BooPointFound();

        if (sfxVariants.Length != 0)
            Sfx.Play(sfxVariants[Random.Range(0, sfxVariants.Length)]);

        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red.WithA(0.3f);
        Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
