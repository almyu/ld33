using UnityEngine;
using JamSuite.Audio;

public class Robot : MonoBehaviour {

    public Animation headAnimation;
    public string alarmSfx = "RobotSpotsPlayer";
    public float alarmSfxThrottle = 3f;
    public Vector2 alarmSfxVolumeRange = new Vector2(0.5f, 0.7f);

    public void OnPlayerSpotted() {
        SetAnimationSpeed(headAnimation, 0f);
        Sfx.Play(alarmSfx, Random.Range(alarmSfxVolumeRange[0], alarmSfxVolumeRange[1]), alarmSfxThrottle);
    }

    public void OnPlayerLost() {
        SetAnimationSpeed(headAnimation, 1f);
    }

    public void SetAnimationSpeed(Animation anim, float speed) {
        foreach (AnimationState state in anim)
            state.speed = speed;
    }
}
