using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Actuator : MonoBehaviour {

    public string inputButton = "Interact";
    public UnityEvent action;

    private bool inRange;

    private void OnValidate() {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        // TODO: input hint
        inRange = true;
    }

    private void OnTriggerExit(Collider other) {
        inRange = false;
    }

    private void Update() {
        if (!inRange) return;

        if (Input.GetButtonDown(inputButton))
            action.Invoke();
    }
}
