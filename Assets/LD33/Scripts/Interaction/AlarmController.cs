using JamSuite.Audio;
using UnityEngine;

public class AlarmController : MonoSingleton<AlarmController> {

    public string sfx;
    public float alarmSfxThrottle = 3.0f;
    private int _lightCounter = 0;
    private MeshRenderer _meshRenderer;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void ShowAlert(bool show) {
        if(show) {
            Sfx.Play(sfx, 0.7f, alarmSfxThrottle);
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
