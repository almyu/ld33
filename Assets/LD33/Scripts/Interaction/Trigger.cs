using UnityEngine;
using UnityEngine.Events;
using JamSuite.UI;

public class Trigger : MonoBehaviour {

    public string text = "Boo!";
    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other) {
        FloatingText.instance.Spawn(transform.position, 0).text = text;
        onTrigger.Invoke();
        GameOverController.instance.BooPointFound();
        Destroy(gameObject);
    }
}
