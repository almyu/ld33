using UnityEngine;
using JamSuite.Audio;

public class PlayerStepSfx : MonoBehaviour {

    public string sfxName = "Step";
    public Vector2 volumeRange = new Vector2(0.8f, 1f);

    public void PlayStep() {
        Sfx.Play(sfxName, Random.Range(volumeRange[0], volumeRange[1]));
    }
}
