﻿using UnityEngine;
using System.Collections.Generic;

namespace JamSuite.Audio {

    [RequireComponent(typeof(AudioSource))]
    public class SfxPlayer : MonoSingleton<SfxPlayer> {

        public SfxList list;
        public float throttle = 0.1f;

        private AudioSource source;
        private Dictionary<AudioClip, float> lastPlays = new Dictionary<AudioClip, float>();


        private void OnValidate() {
            if (!list) {
                var lists = Resources.FindObjectsOfTypeAll<SfxList>();
                if (lists.Length > 0)
                    list = lists[0];
            }
            source = GetComponent<AudioSource>();
        }


        public void Play(string clipName) {
            Play(clipName, 1f);
        }

        public void Play(string clipName, float volumeScale) {
            var clip = list.LookupClip(clipName);
            if (!clip) return;

            var lastPlay = 0f;
            var everPlayed = lastPlays.TryGetValue(clip, out lastPlay);

            if (lastPlay + throttle > Time.timeSinceLevelLoad) return;

            if (everPlayed) lastPlays[clip] = Time.timeSinceLevelLoad;
            else lastPlays.Add(clip, Time.timeSinceLevelLoad);

            source.PlayOneShot(clip, volumeScale);
        }
    }
}
