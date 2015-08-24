using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TriggerBar : MonoSingleton<TriggerBar> {

    public RectTransform template;

    public Trigger[] triggers;

    private void Awake() {
        triggers = FindObjectsOfType<Trigger>();

        for (int i = 0; i < triggers.Length; ++i)
            InstantiateToggle(triggers[i], i);
    }

    private void InstantiateToggle(Trigger trigger, int position) {
        var xf = Instantiate(template);
        xf.pivot = xf.pivot.WithX(position);

        var toggle = xf.GetComponentInChildren<Toggle>();
        if (toggle) trigger.onTrigger.AddListener(() => toggle.isOn = true);
    }
}
