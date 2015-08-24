using JamSuite.Audio;
using UnityEngine;

public class AlarmController : MonoSingleton<AlarmController> {

    public string sfx;

    private int _lightCounter = 0;
    private MeshRenderer _meshRenderer;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void ShowAlert(bool show) {
        if(show) {
            Sfx.Play(sfx);
        }
        _meshRenderer.enabled = show;
    }

    public void ISeeMonster() {
        _lightCounter += 1;
        ShowAlert(true);
    }

    public void ILostMonster() {
        if(_lightCounter != 0) {
            _lightCounter -= 1;
        }
        if(_lightCounter == 0)
            ShowAlert(false);
    }
}
