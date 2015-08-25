using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

    public AudioMixer mixer;
    public Slider musicVolumeSlider, effectsVolumeSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("MusicVolume")) mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.HasKey("EffectsVolume")) mixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);

        var musicVolume = 0f;
        var effectsVolume = 0f;

        mixer.GetFloat("MusicVolume", out musicVolume);
        mixer.GetFloat("EffectsVolume", out effectsVolume);

        musicVolumeSlider.value = musicVolume;
        effectsVolumeSlider.value = effectsVolume;
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
