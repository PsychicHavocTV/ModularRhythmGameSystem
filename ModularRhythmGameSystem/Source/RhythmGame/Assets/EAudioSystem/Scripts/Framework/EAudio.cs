#region Namespaces
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
#endregion

namespace EAudioSystem
{
    public class EAudio
    {
        #region Private & Protected Variables

        static AudioSource[] audioSources = new AudioSource[0];

        
        #endregion

        #region Public Variables

        public static int sourcesCount = 0;
        public static int levelsCount = 0;

        public static int levelStartDelaySeconds = 0;

        public static ScriptableObjectHandler[] levels = new ScriptableObjectHandler[0];

        public static bool isLevelStarting = false;
        public static ScriptableObjectHandler selectedLevel;

        public static bool levelStartDelayFinished = true;

        public static AudioSource source;

        public static bool readyToLoadLevel = false;

        public static IList<ScriptableObjectHandler> loadedLevels = new List<ScriptableObjectHandler>();

        public static ScriptableObjectHandler currentLevel = null;

        
        #endregion

        #region Public Functions

        /// <summary>
        /// Calculate the seconds-per-beat float variable, by dividing the BPM of the song/track by 60.
        /// </summary>
        public static float CalculateSecPerBeat(float songBPM)
        {
            float secPerBeat = 0;
            secPerBeat = 60.0f / songBPM;
            return secPerBeat;
        }

        public static float CalculateDSPSongTime()
        {
            float dspSongTime = 0;
            dspSongTime = (float)AudioSettings.dspTime;
            return dspSongTime;
        }

        /// <summary>
        /// UNSUSED - Depricated.
        /// </summary>
        public static void CalculateMusicInformation(float secPerBeat, float dspSongTime, float songBPM)
        {
            secPerBeat = 60f / songBPM;

            dspSongTime = (float)AudioSettings.dspTime;

            return;
        }

        /// <summary>
        /// Calculate and return the current position in the song/track using DSPTime.
        /// </summary>
        public static float CalculateSongPosition(float dspSongTime)
        {
            // Calculate the position in seconds.
            float songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            return songPosition;
        }

        /// <summary>
        /// Calculate and return the current position in the song/track relative to beats, using the song position and seconds per beat variables.
        /// </summary>
        public static float CalculateSongPosInBeats(float songPosition, float secPerBeat)
        {
            float songPosInBeats = 0;
            songPosInBeats = songPosition / secPerBeat;

            return songPosInBeats;
        }


        /// <summary>
        /// Calculate and return how many seconds long the audio clip is.
        /// </summary>
        public static int CalculateSongLengthSeconds(AudioClip song)
        {
            int seconds = 0;
            seconds = (int)song.length;
            return seconds;

        }

        /// <summary>
        /// Increase or decrease the speed of the selected sound, while keeping the same pitch using the audio mixer.
        /// </summary>
        public static void SetSoundSpeed(AudioMixerGroup mixer, AudioSource source, float speed)
        {
            source.pitch = speed;

            float currentMixerPitch = 0;
            mixer.audioMixer.GetFloat("Pitch", out currentMixerPitch);

            if (speed > 1)
            {
                currentMixerPitch -= speed;
            }
            else if (speed < 1)
            {
                currentMixerPitch += speed;
            }

            mixer.audioMixer.SetFloat("Pitch", currentMixerPitch);
            return;
        }

        public static void ListAudioSources(bool ListParentLocations)
        {
            Debug.Log("Objects With Audio Sources:");
            for (int i = 0; i < sourcesCount; i++)
            {
                Debug.Log(audioSources[i]);
            }
        }

        /// <summary>
        /// Trigger the given sound effect / audio clip.
        /// </summary>
        public static  void TriggerSound(AudioSource soundSource, bool resetIfPlaying)
        {
            if (soundSource.isPlaying == false)
            {
                soundSource.Play();
                return;
            }
            else if (soundSource.isPlaying == true)
            {
                if (resetIfPlaying == true)
                {
                    soundSource.Stop();
                    soundSource.Play();
                    return;
                }
                else
                {
                    return;
                }
            }
        }


        /// <summary>
        /// Check whether or not the size of the given array is the same as the size of the 'audioSources' array.
        /// </summary>
        public static bool checkSizeMatch(AudioSource[] sources)
        {

            if (audioSources.Length == sources.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check whether or not the given Audio Source is already in the 'audioSources' array.
        /// </summary>
        public static bool CheckIsInList(AudioSource source)
        {
            foreach (AudioSource aSource in audioSources)
            {
                if (aSource == source)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update the 'audioSources' array to be a duplicate of the given 'newSourcesList' array.
        /// </summary>
        public static void CopySourcesList(AudioSource[] newSourcesList)
        {
            sourcesCount = 0;

            audioSources = new AudioSource[newSourcesList.Length];
            for (int i = 0; i < newSourcesList.Length; i++)
            {
                audioSources[i] = newSourcesList[i];
            }

            // Now count how many sources are in the new list.
            foreach (AudioSource aSource in audioSources)
            {
                sourcesCount++;
            }
        }

        public static void CopyLevelsList(ScriptableObjectHandler[] newLevelsList)
        {
            levelsCount = 0;

            levels = new ScriptableObjectHandler[newLevelsList.Length];
            for (int i = 0; i < newLevelsList.Length; i++)
            {
                levels[i] = newLevelsList[i];
            }

            // Now count how many levels are in the newly created list.
            foreach (ScriptableObjectHandler level in levels)
            {
                levelsCount++;
            }
        }

        public static void AddAudioSource(AudioSource source)
        {
            AudioSource[] tempSources = new AudioSource[audioSources.Length];
            for (int i = 0; i < audioSources.Length - 1; i++)
            {
                if (audioSources[i] != null)
                {
                    tempSources[i] = audioSources[i];
                }
            }

            audioSources = new AudioSource[audioSources.Length + 1];
            for (int i = 0; i < tempSources.Length; i++)
            {
                if (tempSources[i] != null)
                {
                    audioSources[i] = tempSources[i];
                }
            }

            sourcesCount = 0;

            foreach (AudioSource aSource in audioSources)
            {
                sourcesCount++;
            }

            Debug.Log("Source Count: " + sourcesCount);

            audioSources[sourcesCount - 1] = source;
        }
        

        

        #endregion
    }
}

