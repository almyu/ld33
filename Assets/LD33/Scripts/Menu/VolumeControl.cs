using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

    public AudioMixer mixer;
    public Slider musicVolumeSlider, effectsVolumeSlider;

    private void Start() {
        var musicVolume = 0f;
        var effectsVolume = 0f;

        if (PlayerPrefs.HasKey("MusicVolume")) {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            mixer.SetFloat("MusicVolume", musicVolume);
        }
        else mixer.GetFloat("MusicVolume", out musicVolume);


        if (PlayerPrefs.HasKey("EffectsVolume")) {
            effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
            mixer.SetFloat("EffectsVolume", effectsVolume);
        }
        else mixer.GetFloat("EffectsVolume", out effectsVolume);
    

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);

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
