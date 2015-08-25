using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

    public AudioMixer mixer;

    private void Start() {
        if (PlayerPrefs.HasKey("MusicVolume")) mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.HasKey("EffectsVolume")) mixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));
    }

    public void SetMusicVolume(float vol) {
        mixer.SetFloat("MusicVolume", vol);
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    public void SetEffectsVolume(float vol) {
        mixer.SetFloat("EffectsVolume", vol);
        PlayerPrefs.SetFloat("EffectsVolume", vol);
    }
}
