using UnityEngine;
using UnityEngine.Events;
using JamSuite.UI;

[RequireComponent(typeof(SphereCollider))]
public class Trigger : MonoBehaviour {

    public string text = "Boo!";
    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other) {
        FloatingText.instance.Spawn(transform.position, 0).text = text;
        onTrigger.Invoke();
        ScoreCounter.instance.BooPointFound();
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red.WithA(0.3f);
        Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
