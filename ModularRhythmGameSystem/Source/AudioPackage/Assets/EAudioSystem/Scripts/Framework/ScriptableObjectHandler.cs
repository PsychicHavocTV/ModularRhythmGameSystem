using UnityEngine;

namespace EAudioSystem
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class ScriptableObjectHandler : ScriptableObject
    {
        public string levelName;
    
        public AudioClip levelSong;
        public float songBPM;
        public string songName;
        public string songArtist;

    }
}
