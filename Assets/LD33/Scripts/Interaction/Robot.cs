using UnityEngine;
using JamSuite.Audio;

public class Robot : MonoBehaviour {

    public Animation headAnimation;
    public string alarmSfx = "RobotSpotsPlayer";

    public void OnPlayerSpotted() {
        SetAnimationSpeed(headAnimation, 0f);
        Sfx.Play(alarmSfx);
    }

    public void OnPlayerLost() {
        SetAnimationSpeed(headAnimation, 1f);
    }

    public void SetAnimationSpeed(Animation anim, float speed) {
        foreach (AnimationState state in anim)
            state.speed = speed;
    }
}
