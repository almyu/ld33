using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JamSuite.Audio {

    [RequireComponent(typeof(Crossfader), typeof(AudioSource))]
    public class BgmPlayer : MonoSingleton<BgmPlayer> {

        public Crossfader fader;
        public BgmList playlists;

        private AudioSource source;

        private class Snapshot {
            public AudioClip track;
            public float trackTime;
        }

        private Dictionary<Playlist, Snapshot> snapshots = new Dictionary<Playlist, Snapshot>();
        private List<Playlist> stack = new List<Playlist>();

        public Playlist activePlaylist {
            get { return stack.Count != 0 ? stack[stack.Count - 1] : null; }
        }


        private void OnValidate() {
            if (!playlists) {
                var lists = Resources.FindObjectsOfTypeAll<BgmList>();
                if (lists.Length > 0)
                    playlists = lists[0];
            }
            if (!fader) fader = GetComponent<Crossfader>();
        }

        private void Awake() {
            if (instance && instance != this) {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
        }

        private void Update() {
            if (stack.Count == 0) return;
            if (source.clip == null) return;

            if (source.time + fader.duration >= source.clip.length) {
                var playlist = stack[stack.Count - 1];

                var snapshot = GetSnapshot(playlist);
                snapshot.track = playlist.GetNextTrack(snapshot.track);
                snapshot.trackTime = 0f;

                Play(snapshot);
            }
        }


        public void PushPlaylist(string playlistName) {
            PushPlaylist(playlists.LookupPlaylist(playlistName));
        }

        public void PopPlaylist(string playlistName) {
            PopPlaylist(playlists.LookupPlaylist(playlistName));
        }


        public void PushPlaylist(Playlist playlist) {
            ChangePlaylist(activePlaylist, playlist);
            stack.Add(playlist);
        }

        public void PopPlaylist(Playlist playlist) {
            var index = stack.LastIndexOf(playlist);
            if (index == -1) return;

            stack.RemoveAt(index);

            if (index == stack.Count) // was active
                ChangePlaylist(playlist, activePlaylist);
        }

        private void ChangePlaylist(Playlist from, Playlist to) {
            if (from != null) {
                var snapshot = GetSnapshot(from);
                snapshot.track = source.clip;
                snapshot.trackTime = source.time;
            }
            if (to != null) {
                var snapshot = GetSnapshot(to);

                Debug.LogFormat("Now playing: {0} — {1} at {2:c}",
                    to.name, snapshot.track.name,
                    System.TimeSpan.FromSeconds(snapshot.trackTime));

                Play(snapshot);
            }
            else {
                Debug.LogFormat("Now playing: silence");
                Stop();
            }
        }

        private void Play(Snapshot snapshot) {
            source = fader.Crossfade(source, snapshot.track);
            source.time = snapshot.trackTime;
        }

        private void Stop() {
            source = fader.Crossfade(source, default(AudioClip));
        }

        private Snapshot GetSnapshot(Playlist playlist) {
            var snapshot = default(Snapshot);
            if (snapshots.TryGetValue(playlist, out snapshot)) return snapshot;

            snapshot = new Snapshot {
                track = playlist.GetNextTrack(null),
                trackTime = 0f
            };
            snapshots.Add(playlist, snapshot);

            return snapshot;
        }
    }
}
