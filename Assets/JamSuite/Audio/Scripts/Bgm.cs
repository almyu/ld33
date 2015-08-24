using UnityEngine;

namespace JamSuite.Audio {

    public class Bgm : MonoBehaviour {

        public static void PushPlaylist(string playlistName) {
            var player = BgmPlayer.instance;
            if (player) player.PushPlaylist(playlistName);
        }

        public static void PopPlaylist(string playlistName) {
            var player = BgmPlayer.instance;
            if (player) player.PopPlaylist(playlistName);
        }


        public string playlistName;
        public bool playOnStart = true;

        private void OnEnable() {
            if (!playOnStart) PushPlaylist(playlistName);
        }

        private void Start() {
            if (playOnStart) PushPlaylist(playlistName);
        }

        private void OnDisable() {
            if (!playOnStart) PopPlaylist(playlistName);
        }
    }
}
