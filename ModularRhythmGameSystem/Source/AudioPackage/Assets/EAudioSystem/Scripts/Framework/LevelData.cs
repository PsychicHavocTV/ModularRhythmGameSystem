using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EAudioSystem;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace EAudioSystem
{
    public class LevelData : MonoBehaviour
    {
        //public static LevelData instance;
        public static ScriptableObjectHandler levelData;

        static float[] notes;

        static int nextIndex = 0;

        public static float beatsShownInAdvance = 1;

        static int lastBeat = -1;

        [SerializeField] GameObject[] levelObjects;
    
        public static AudioSource source;

        public AudioSource musicPlayer;
    
        public static float levelSpeed = 1;

        public static bool spawnNote = false;

        public static float posInBeats = 0;

        private void Start()
        {
            source = musicPlayer;
            Debug.Log(source.gameObject.name);
        }

        /// <summary>
        /// Re-create the 'notes' float array as a copy of another float array containing all of the note timings.
        /// </summary>
        public static void SetNotemap(float[] newNotemap)
        {
            Debug.Log(newNotemap.Length);
            notes = new float[newNotemap.Length];
            for (int i = 0; i < notes.Length; i++)
            {
                notes[i] = newNotemap[i];
            }
        }

        /// <summary>
        /// Get and return the current note.
        /// </summary>
        public static float GetCurrentBeat()
        {
            float currentBeat = notes[nextIndex - 1];
            return currentBeat;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CheckNoteSpawn(ScriptableObjectHandler level, float songPosition, float songPosInBeats, float dspSongTime, float secPerBeat, bool shouldSpawn)
        {
            if (notes.Length < 1)
            {
                notes = new float[6];
                notes[0] = 1.0f; notes[1] = 2.0f; notes[2] = 2.5f;
                notes[3] = 3.0f; notes[4] = 3.5f; notes[5] = 4.5f;
            }

            if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
            {
                // Spawn in the note. for now, just Debug.
                Debug.Log("Spawn In Note.");
                spawnNote = true;
                nextIndex++;
            }
            
        }

        /// <summary>
        /// Set how long the application should wait before playing audio.
        /// </summary>
        public static void SetPreSongTimer(int duration)
        {
            EAudioSystem.EAudio.levelStartDelaySeconds = duration;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void StartLevel(ScriptableObjectHandler level)
        {
            source.clip = levelData.levelSong;
            EAudioSystem.EAudio.isLevelStarting = true;
            EAudioSystem.EAudio.selectedLevel = level;
            source.Play();

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AssignLevelDataFromObject(ScriptableObjectHandler level)
        {
            if (level.levelSong != null)
                source.clip = level.levelSong;
            return;
        }
    }
}
